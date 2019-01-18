using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pigeon.DataAccess.Repository;
using Pigeon.WebApi.Models;
using Pigeon.Framework.Exceptions;
using Pigeon.DataAccess.Entities;
using Pigeon.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Pigeon.WebApi.WebApi;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pigeon.WebApi.Controllers
{
    [Route(Urls.V1.ApiBase + "[controller]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<PigeonUser> _userManager;
        private readonly SignInManager<PigeonUser> _signInManager;
        private IPasswordHasher<PigeonUser> _hasher;
        private IConfiguration _config;
        private IEmailClient _emailClient;
        IUserInstituteRepository _userInstituteRepository;

        public AccountController(UserManager<PigeonUser> userManager,
            SignInManager<PigeonUser> signInManager, IPasswordHasher<PigeonUser> hasher,
            IConfiguration config, IEmailClient emailClient, IUserInstituteRepository userInstituteRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hasher = hasher;
            _config = config;
            _emailClient = emailClient;
            _userInstituteRepository = userInstituteRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]RegistrationModel model)
        {
            var errorMsg = "Failed to create account";
            if (ModelState.IsValid)
            {
                var user = new PigeonUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    CountryCode = model.CountryCode,
                    MobileNo = model.MobileNo,
                    PricingTypeId = 1
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok(model);
                } else
                {
                    if (result.Errors != null && result.Errors.ToList().Count > 0)
                    {
                        errorMsg = string.Join("; ", result.Errors.ToList().Select(m => m.Description).ToArray());
                    }
                    
                }
            }
            return BadRequest(errorMsg);
        }

        [HttpPut("{userName}")]
        [Authorize]
        public async Task<IActionResult> Put(string userName, [FromBody]UserProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userName);
                user.MobileNo = model.MobileNo;
                user.CountryCode = model.CountryCode;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return NoContent();
                }
            }
            return BadRequest("Failed to create account");
        }

        [HttpPatch("currentInstitute")]
        [Authorize]
        public async Task<IActionResult> SetCurrentInstitute([FromBody]int? instituteId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(this.ClientId);
                //user.InstituteId = instituteId;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return NoContent();
                }
            }
            return BadRequest("Failed to create account");
        }

        [HttpPost("OneTimePassword/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOneTimePassword(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var password = PigeonSecurity.GeneratePassword(8);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, password);
                if (result.Succeeded)
                {
                    return await SendOtpCode(userName, password);
                }
            }
            else
            {
                return await SendOtpCode(userName, password);
            }
            return BadRequest("Failed to generate password");
        }



        private async Task<IActionResult> SendOtpCode(string userName, string password)
        {
            var body = $"Dear {userName}, your OTP is {password}";
            await _emailClient.SendEmailAsync(userName, "Your OTP password", body);
            return Ok(new
            {
                IsOtpSent = true
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {            
            var (claims, user) = await GetTokenFor(model);
            if (claims != null)
            {
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Ok(new
                {                    
                    userName = user.UserName                    
                });
            }
            return BadRequest("Failed to login");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);

                    var claims = new[]
                    {
                          new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.Email, user.Email),
                          new Claim(JwtRegisteredClaimNames.Sid, user.Id)
                        }.Union(userClaims);

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                       issuer: _config["Tokens:Issuer"],
                       audience: _config["Tokens:Audience"],
                       claims: claims,
                       expires: DateTime.UtcNow.AddYears(1),
                       signingCredentials: creds
                       );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        model = user
                    });
                }
            }
            else
            {
                return BadRequest("No user found!");
            }
            return BadRequest("Failed to generate token");
        }

        private async Task<(IEnumerable<Claim>, PigeonUser)> GetTokenFor(CredentialModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);

                    var claims = new[]
                    {
                          new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.Email, user.Email),
                          new Claim(JwtRegisteredClaimNames.Sid, user.Id)
                        }.Union(userClaims);
                    return (claims, user);
                }
            }
            return (null, null);
        }


        [HttpPost("addinstitutes")]
        public async Task<IActionResult> AddInstitutes([FromBody]int[] institutes)
        {
            if (institutes == null || institutes.Count() == 0)
            {
                return BadRequest("Send at least one institute.");
            }
            var userInstitutes = new List<UserInstitute>();
            userInstitutes.AddRange(institutes.Select(x => new UserInstitute() { InstituteId = x, UserId = this.ClientId }));
            this._userInstituteRepository.Save(userInstitutes);
            return Ok();
        }

        [HttpGet("institutes")]
        public async Task<IActionResult> GetInstitutes()
        {
            var institutes = this._userInstituteRepository.GetBy(x => x.UserId == this.ClientId)
                .Select(x =>
                {
                    var dto = new InstituteDto();
                    dto.Map(x.Institute);
                    return dto;
                });
            return Ok(institutes);
        }

    }
}