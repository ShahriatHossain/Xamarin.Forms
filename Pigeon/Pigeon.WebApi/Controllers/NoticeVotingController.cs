using Microsoft.AspNetCore.Mvc;
using Pigeon.DataAccess.Entities;
using Pigeon.DataAccess.Repository;
using Pigeon.Framework.Exceptions;
using Pigeon.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Controllers
{
    [Route(Urls.V1.ApiBase + "notice")]
    public class NoticeVotingController: BaseController
    {
        private INoticeVotingRepository _noticeVotingRepository;
        private INoticeRepository _noticeRepository;
        public NoticeVotingController(INoticeVotingRepository noticeVotingRepository, INoticeRepository noticeRepository)
        {
            this._noticeVotingRepository = noticeVotingRepository;
            this._noticeRepository = noticeRepository;
        }

        [HttpPost("{noticeId}/voting/{responseType}")]
        public IActionResult CastVote(int noticeId, string responseType)
        {
            var notice = this._noticeRepository.GetById(noticeId);
            if(notice == null)
            {
                throw new ResourceObjectNotFoundException(typeof(Notice).Name, noticeId);
            }
            if(!notice.IsVotingEnabled)
            {
                throw new BadInputParameterValueException("noticeId", "Voting is not enabled.");
            }
            if (notice.VotingLastDate.HasValue && notice.VotingLastDate.Value < DateTime.Now)
            {
                throw new BadInputParameterValueException("noticeId", "Voting time is expired");
            }
            var votingType = responseType?.ToUpper() == "UP" ? VotingType.Up : VotingType.Down;
            var noticeVoting = new NoticeVoting()
            {
                ChannelId = notice.ChannelId,
                NoticeId = noticeId,
                UserId = this.ClientId,
                ResponseType = votingType
            };
            this._noticeVotingRepository.Save(noticeVoting);
            return Ok();
        }

        [HttpGet("{noticeId}/voting/result")]
        public IActionResult Get(int noticeId, string responseType)
        {
            var notice = this._noticeRepository.GetById(noticeId);
            if (notice == null)
            {
                throw new ResourceObjectNotFoundException(typeof(Notice).Name, noticeId);
            }
            if (!notice.IsVotingEnabled)
            {
                throw new BadInputParameterValueException("noticeId", "Voting is not enabled.");
            }

            int upVoteCount;
            int downVoteCount;

            this._noticeVotingRepository.GetResult(noticeId, out upVoteCount, out downVoteCount);
            return Ok(new { upVoteCount = upVoteCount, downVoteCount = downVoteCount });
        }
    }
}
