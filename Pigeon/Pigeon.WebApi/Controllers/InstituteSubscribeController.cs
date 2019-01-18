using Microsoft.AspNetCore.Mvc;
using Pigeon.DataAccess.Entities;
using Pigeon.DataAccess.Repository;
using Pigeon.Framework.Exceptions;
using Pigeon.WebApi.Models;
using System.Linq;

namespace Pigeon.WebApi.Controllers
{
    [Route(Urls.V1.ApiBase + "Institute")]
    public class InstituteSubscribeController : BaseController
    {
        private IInstituteSubscribeRepository _instituteSubscribeRepository;
        public InstituteSubscribeController(IInstituteSubscribeRepository instituteSubscribeRepository)
        {
            _instituteSubscribeRepository = instituteSubscribeRepository;
        }

        [HttpPost("subscribe/{instituteId}")]
        public IActionResult SubscribeChannel(int instituteId)
        {
            var subscribe = new InstituteSubscribe();
            subscribe.UserId = ClientId;
            subscribe.InstituteId = instituteId;
            this._instituteSubscribeRepository.Save(subscribe);
            return Ok();
        }
        [HttpPost("unsubscribe/{instituteId}")]
        public IActionResult UnsubscribeChannel(int instituteId)
        {
            var instituteSubscribe = this._instituteSubscribeRepository.GetBy(x => x.InstituteId == instituteId && x.UserId == ClientId).FirstOrDefault();
            if (instituteSubscribe == null)
            {
                throw new ResourceObjectNotFoundException(typeof(Institute).Name, instituteId);
            }
            this._instituteSubscribeRepository.Delete(instituteSubscribe);
            return Ok();
        }

        [HttpGet("subscribers/{instituteId}")]
        public IActionResult GetSubscribers(int instituteId)
        {
            var subscribers = this._instituteSubscribeRepository.GetBy(x => x.InstituteId == instituteId);

            return Ok(subscribers);
        }
    }
}
