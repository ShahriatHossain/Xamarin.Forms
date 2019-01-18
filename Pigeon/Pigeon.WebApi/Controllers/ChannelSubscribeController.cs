using Microsoft.AspNetCore.Mvc;
using Pigeon.DataAccess.Entities;
using Pigeon.DataAccess.Repository;
using Pigeon.Framework.Exceptions;
using Pigeon.WebApi.Models;
using System.Linq;

namespace Pigeon.WebApi.Controllers
{
    [Route(Urls.V1.ApiBase + "Channel")]
    //[Route(Urls.V1.ApiBase + "[controller]")]
    public class ChannelSubscribeController : BaseController
    {
        private IChannelSubscribeRepository _channelSubscribeRepository;
        public ChannelSubscribeController(IChannelSubscribeRepository channelSubscribeRepository)
        {
            this._channelSubscribeRepository = channelSubscribeRepository;
        }

        [HttpPost("subscribe/{channelId}")]
        public IActionResult SubscribeChannel(int channelId)
        {
            var subscribe = new ChannelSubscribe();
            subscribe.UserId = ClientId;
            subscribe.ChannelId = channelId;
            this._channelSubscribeRepository.Save(subscribe);
            return Ok();
        }
        [HttpPost("unsubscribe/{channelId}")]
        public IActionResult UnsubscribeChannel(int channelId)
        {
            var channelSubscribe = this._channelSubscribeRepository.GetBy(x => x.ChannelId == channelId && x.UserId == ClientId).FirstOrDefault();
            if (channelSubscribe == null)
            {
                throw new ResourceObjectNotFoundException(typeof(Channel).Name, channelId);
            }
            this._channelSubscribeRepository.Delete(channelSubscribe);
            return Ok();
        }

        [HttpGet("subscribers/{channelId}")]
        public IActionResult GetSubscribers(int channelId)
        {
            var subscribers = this._channelSubscribeRepository.GetBy(x => x.ChannelId == channelId);

            return Ok(subscribers);
        }
    }
}
