using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pigeon.DataAccess.Entities;
using Pigeon.DataAccess.Repository;
using Pigeon.Framework;
using Pigeon.Framework.Exceptions;
using Pigeon.Framework.Extensions;
using Pigeon.WebApi.Models;
using Pigeon.WebApi.Service;
using Pigeon.WebApi.WebApi;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Controllers
{
    [Route(Urls.V1.ApiBase + "[controller]")]
    [Consumes("application/json", "application/json-patch+json", "application/x-www-form-urlencoded", "multipart /form-data")]
    public class NoticeController : BaseController
    {
        private INoticeRepository _noticeRepository;
        private IAccessControlService _accessControlService;
        private IFileStorageService _fileStorageService;
        private INoticeVotingRepository _noticeVotingRepository;

        public NoticeController(INoticeRepository noticeRepository, IAccessControlService accessControlService,
            IFileStorageService fileStorageService, INoticeVotingRepository noticeVotingRepository)
        {
            this._noticeRepository = noticeRepository;
            this._accessControlService = accessControlService;
            this._fileStorageService = fileStorageService;
            _noticeVotingRepository = noticeVotingRepository;
        }


        private NoticeDto ToDto(Notice entity)
        {
            var dto = new NoticeDto();
            dto.Map(entity);
            return dto;
        }


        [HttpPost]
        [ValidateModel]
        public IActionResult Post([FromBody]TextNotice item)
        {
            //if (!_accessControlService.CanCreateNotice(item.ChannelId))
            //{
            //    throw new QuotaExceededException();
            //}
            item.CreatorId = this.ClientId;
            var notice = this._noticeRepository.Save(item);
            return Ok(ToDto(notice));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var channelId = Request.Query["channelId"].ToNullableInt();
            var noticeId = Request.Query["noticeId"].ToNullableInt();


            var predicate = PredicateBuilder.True<Notice>();

            if (channelId.HasValue)
            {
                predicate = predicate.And(x => x.ChannelId == channelId.Value);
            }

            predicate = predicate.And(x => x.CreatorId == this.ClientId || x.Channel.ChannelSubscribes.Any(y => y.UserId == this.ClientId));

            var lastReceivednoticeId = Request.Query["lastReceivednoticeId"].ToNullableInt();
            if (lastReceivednoticeId.HasValue)
            {
                predicate = predicate.And(x => x.Id > lastReceivednoticeId.Value);
            }

            if (noticeId.HasValue)
            {
                predicate = predicate.And(x => x.Id == noticeId);
            }

            var notices = this._noticeRepository.GetBy(predicate).Select(ToDto).ToList();

            SetVotingStatusForNotice(notices);

            return Ok(notices);
        }

        private void SetVotingStatusForNotice(System.Collections.Generic.List<NoticeDto> notices)
        {
            for (int i = 0; i < notices.Count; i++)
            {
                var noticeVoting = _noticeVotingRepository.GetBy(x => x.UserId == this.ClientId && x.NoticeId == notices[i].Id).FirstOrDefault();
                if (noticeVoting != null)
                {
                    notices[i].VotingStatus = "vote casted";
                }
                else
                {
                    if (!(notices[i].VotingLastDate is null) && (DateTime.Now > notices[i].VotingLastDate))
                    {
                        notices[i].VotingStatus = "voting over";
                    }
                }
            }
        }

        [HttpPost("media/channel/{channelId}/{isVotingEnabled}")]
        public async Task<IActionResult> PostMediaNotice(int channelId, bool isVotingEnabled, IFormFile file)
        {
            if (file == null)
            {
                file = Request.Form.Files.First();
            }

            var name = file.FileName;
            //if (!_accessControlService.CanCreateNotice(channelId))
            //{
            //    throw new QuotaExceededException();
            //}

            var dateTime = Request.Query["votingLastDate"];
            var votingLastDate = string.IsNullOrEmpty(dateTime) ? (DateTime?)null
                : DateTime.ParseExact(dateTime.ToString(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);

            var mediaNotice = new MediaNotice()
            {
                ChannelId = channelId,
                FileDisplayName = name,
                FileType = Path.GetExtension(name),
                CreatorId = this.ClientId,
                IsVotingEnabled = isVotingEnabled,
                VotingLastDate = votingLastDate
            };

            string blobName = Guid.NewGuid() + mediaNotice.FileType;
            using (var stream = file.OpenReadStream())
            {
                await this._fileStorageService.Upload(stream, blobName);
            }
            mediaNotice.FileName = blobName;
            var notice = this._noticeRepository.Save(mediaNotice);
            return Ok(ToDto(notice));
        }
    }
}
