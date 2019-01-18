using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;
using Pigeon.WebApi.WebApi;
using System.IdentityModel.Tokens.Jwt;

namespace Pigeon.WebApi.Controllers
{
    [ApiExceptionFilter]    
    public abstract class BaseController: Controller
    {
        protected static Logger Logger = LogManager.GetCurrentClassLogger();
        protected BaseController()
        {

        }
        public string ClientId => this.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sid).Value;
    }
}
