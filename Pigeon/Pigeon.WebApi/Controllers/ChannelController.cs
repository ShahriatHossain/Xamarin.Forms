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
    public class ChannelController : CrudController<Channel, ChannelDto>
    {
        private IChannelRepository _channelRepository;
        private IChannelSubscribeRepository _channelSubscribeRepository;
        private IAccessControlService _accessControlService;
        private IChannelCategoryRepository _channelCategoryRepository;
        private IFileStorageService _fileStorageService;
        private INoticeRepository _noticeRepository;

        public ChannelController(IChannelRepository channelRepository,
            IChannelSubscribeRepository channelSubscribeRepository, IAccessControlService accessControlService,
            IChannelCategoryRepository channelCategoryRepository, IFileStorageService fileStorageService,
            INoticeRepository noticeRepository)
        {
            this._channelRepository = channelRepository;
            this._channelSubscribeRepository = channelSubscribeRepository;
            this._accessControlService = accessControlService;
            this._channelCategoryRepository = channelCategoryRepository;
            this._fileStorageService = fileStorageService;
            this._noticeRepository = noticeRepository;
        }

        protected override IRepository<Channel> Repository => this._channelRepository;

        [HttpGet]
        public override IActionResult Get()
        {
            var creatorId = Request.Query["creatorId"].ToNullableInt();
            var channelId = Request.Query["channelId"].ToNullableInt();
            var instituteId = Convert.ToInt32(Request.Query["instituteId"]);
            var status = Request.Query["status"];
            var lastRefreshTime = Request.Query["lastRefreshTime"];
            var searchText = Request.Query["searchText"];
            var isOwned = Request.Query["isOwnedByMe"].ToNullableBoolean();
            var isSubscribed = Request.Query["isSubscribedByMe"].ToNullableBoolean();
            var isDefault = Request.Query["isDefault"].ToNullableBoolean();

            var predicate = PredicateBuilder.True<Channel>();
            // thi is check
            if (creatorId.HasValue)
            {
                if (creatorId >= 1)
                {
                    predicate = predicate.And(x => x.CreatorId == this.ClientId);
                }
                else
                {
                    predicate = predicate.And(x => x.CreatorId != this.ClientId);
                }
            }

            if (channelId.HasValue)
            {
                predicate = predicate.And(x => x.Id == channelId.Value);
            }

            if (instituteId > 0)
            {
                predicate = predicate.And(x => x.InstituteId == instituteId);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                predicate = predicate.And(x => x.Status == status);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                predicate = predicate.And(x => x.Name.Contains(searchText));
            }

            if (!string.IsNullOrWhiteSpace(lastRefreshTime))
            {
                //predicate = predicate.And(x => x.Name.Contains(searchText));
            }
            if (isSubscribed.HasValue)
            {
                if (isSubscribed.Value)
                {
                    predicate = predicate.And(x => x.ChannelSubscribes.Any(y => y.UserId == this.ClientId));
                }
                else
                {
                    predicate = predicate.And(x => !x.ChannelSubscribes.Any() || x.ChannelSubscribes.Any(y => y.UserId != this.ClientId));
                }
            }
            if (isOwned.HasValue)
            {
                if (isOwned.Value)
                {
                    predicate = predicate.And(x => x.CreatorId == this.ClientId);
                }
                else
                {
                    predicate = predicate.And(x => x.CreatorId != this.ClientId);
                }
            }

            if (isDefault.HasValue)
            {
                if (isDefault.Value)
                {
                    predicate = predicate.And(x => x.IsDefault == isDefault);
                }
                else
                {
                    predicate = predicate.And(x => x.IsDefault != isDefault);
                }
            }

            var channels = this._channelRepository.GetBy(predicate).Select(ToDto).ToList();

            foreach (var item in channels)
            {
                if (item.CreatorId == this.ClientId)
                {
                    item.OwnerFlag = true;
                }
                else if (item.ChannelSubscribes.Any(x => x.UserId == this.ClientId))
                {
                    item.IsSubscribedByMe = true;
                }
                else if (item.CreatorId != this.ClientId && (item.ChannelSubscribes.Any() || item.ChannelSubscribes.Any(y => y.UserId != this.ClientId)))
                {
                    item.IsNotSubscribedAndOwnedByMe = true;
                }

                GetTotalChannelUser(item);
            }

            return Ok(channels);
        }

        private int GetTotalChannelUser(ChannelDto item)
        {
            var pred = PredicateBuilder.True<ChannelSubscribe>().And(x => x.ChannelId == item.Id);
            var channelSubscribes = this._channelSubscribeRepository.GetBy(pred);

            item.TotalChannelUsers = channelSubscribes != null ? channelSubscribes.ToList().Count : 0;

            return item.TotalChannelUsers;
        }

        [HttpGet("subscribe")]
        public IActionResult GetSubscribe()
        {
            var predicate = PredicateBuilder.True<Channel>();
            predicate = predicate.And(x => x.ChannelSubscribes.Any(y => y.UserId == this.ClientId));

            var dateTime = Request.Query["lastRefreshDate"];
            var lastRefreshDate = string.IsNullOrEmpty(dateTime) ? (DateTime?)null
                : DateTime.ParseExact(dateTime.ToString(), "dd/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            var includeOwnChannel = Request.Query["includeOwnChannel"].ToNullableBoolean();
            if (includeOwnChannel.HasValue && includeOwnChannel.Value)
            {
                predicate = predicate.Or(x => x.CreatorId == this.ClientId);
            }
            var channels = this._channelRepository.GetBy(predicate).Select(ToDto).ToList();

            SetChannelOwnerAndCountNotice(channels, lastRefreshDate);

            return Ok(channels);
        }

        private void SetChannelOwnerAndCountNotice(System.Collections.Generic.List<ChannelDto> channels, DateTime? lastRefreshDate)
        {
            for (int i = 0; i < channels.Count; i++)
            {
                if (channels[i].CreatorId == this.ClientId)
                {
                    channels[i].OwnerFlag = true;
                }

                if (lastRefreshDate != null)
                {
                    var isValid = _noticeRepository.IsValidByRefreshDate(channels[i].Id, Convert.ToDateTime(lastRefreshDate));
                    if (isValid)
                    {
                        channels[i].NewNoticeIndicator = "*";
                    }
                }

            }
        }

        [HttpPost]
        [ValidateModel]
        public override IActionResult Post([FromBody]Channel item)
        {
            //if (!_accessControlService.CanCreateChannel())
            //{
            //    throw new QuotaExceededException();
            //}
            item.CreatorId = this.ClientId;
            item.RecordUpdatedOn = DateTime.Now;
            item = this.Repository.Save(item);
            return Ok(ToDto(item)); 
        }

        [HttpPut("{channelId}/securepin/{securepin}")]
        public IActionResult SetSecurePin(int channelId, string securepin)
        {
            if (securepin?.Length != 4)
            {
                return BadRequest("The length of secure pin has to be four.");
            }
            this._channelRepository.SetSecurePin(channelId, securepin);
            return Ok();
        }

        //[HttpGet("CanCreate")]
        //public IActionResult CanCreate()
        //{
        //    return Ok(_accessControlService.CanCreateChannel());
        //}

        //[HttpGet("{channelId}/CanCreateNotice")]
        //public IActionResult CanCreateNotice(int channelId)
        //{
        //    return Ok(_accessControlService.CanCreateNotice(channelId));
        //}

        [HttpGet("ChannelCategories")]
        public IActionResult GetChannelCategories()
        {
            var categories = this._channelCategoryRepository.Get();
            return Ok(categories);
        }

        [HttpPost("PostAndGetBlob")]
        public async Task<IActionResult> PostAndGetBlob(IFormFile file)
        {
            if (file == null)
            {
                file = Request.Form.Files.First();
            }

            var name = file.FileName;
            var fileType = Path.GetExtension(name);

            string blobName = Guid.NewGuid() + fileType;
            using (var stream = file.OpenReadStream())
            {
                await this._fileStorageService.Upload(stream, blobName);
            }

            return Ok(blobName);
        }
    }
}
