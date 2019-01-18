using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pigeon.DataAccess.Entities;
using Pigeon.DataAccess.Repository;
using Pigeon.WebApi.Models;
using Pigeon.WebApi.WebApi;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Service
{
    public class AccessControlService : IAccessControlService
    {
        private IChannelRepository _channelRepository;
        private INoticeRepository _noticeRepository;
        private UserManager<PigeonUser> _userManager;
        private IHttpContextAccessor _httpContext;
        private SecurityDbContext _securityDbContext;
        public AccessControlService(IChannelRepository channelRepository, INoticeRepository noticeRepository,
            UserManager<PigeonUser> userManager, IHttpContextAccessor context, SecurityDbContext securityDbContext)
        {
            _channelRepository = channelRepository;
            _noticeRepository = noticeRepository;
            _userManager = userManager;
            _httpContext = context;
            _securityDbContext = securityDbContext;
        }
        private string ClientId => _httpContext.HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sid).Value;

        //private PricingType PricingType(PigeonUser user)
        //{            
        //    if(user.Institute != null)
        //    {
        //        return user.Institute.PricingType;
        //    }
        //    return user.PricingType;
        //}        

        private PigeonUser GetUser()
        {
            return _securityDbContext.Users
                            //.Include(x => x.PricingType)
                            //.Include(x => x.Institute)
                            //.Include(x => x.Institute.PricingType)
                            .FirstOrDefault(x => x.Id == ClientId);
        }
        
        //public bool CanCreateChannel()
        //{
        //    var user = GetUser();
        //    //var pricingType = PricingType(user);
        //    var channelCount = _channelRepository.Count(user.Id);
        //    return pricingType.CanCreateChannel(channelCount);
        //}
        //public bool CanCreateNotice(int channelId)
        //{
        //    var user = GetUser();
        //    var pricingType = PricingType(user);
        //    var noticeCount = _noticeRepository.Count(channelId);
        //    return pricingType.CanCreateNotice(noticeCount);
        //}
    }
}
