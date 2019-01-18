using Microsoft.AspNetCore.Mvc;
using Pigeon.DataAccess.Entities;
using Pigeon.DataAccess.Repository;
using Pigeon.Framework;
using Pigeon.Framework.Exceptions;
using Pigeon.Framework.Extensions;
using Pigeon.WebApi.Models;
using Pigeon.WebApi.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pigeon.WebApi.Controllers
{
    [Route(Urls.V1.ApiBase + "[controller]")]
    public class InstituteController : CrudController<Institute, InstituteDto>
    {
        private IInstituteRepository _instituteRepository;
        private IInstituteCategoryRepository _instituteCategoryRepository;
        private IInstituteTypeRepository _instituteTypeRepository;
        private IChannelRepository _channelRepository;
        private IInstituteSubscribeRepository _instituteSubscribeRepository;
        private IChannelSubscribeRepository _channelSubscribeRepository;

        public InstituteController(IInstituteRepository instituteRepository, IInstituteCategoryRepository instituteCategoryRepository,
            IInstituteTypeRepository instituteTypeRepository, IChannelRepository channelRepository,
            IInstituteSubscribeRepository instituteSubscribeRepository, IChannelSubscribeRepository channelSubscribeRepository)
        {
            this._instituteRepository = instituteRepository;
            this._instituteCategoryRepository = instituteCategoryRepository;
            this._instituteTypeRepository = instituteTypeRepository;
            this._channelRepository = channelRepository;
            _instituteSubscribeRepository = instituteSubscribeRepository;
            _channelSubscribeRepository = channelSubscribeRepository;
        }

        protected override IRepository<Institute> Repository => this._instituteRepository;

        [HttpPost]
        [ValidateModel]
        public override IActionResult Post([FromBody]Institute item)
        {
            item.CreatorId = this.ClientId;
            item.CreationTime = DateTime.Now;
            item = this.Repository.Save(item);

            if (item != null)
            {
                var noOfChannel = _channelRepository.Get().Where(x => x.InstituteId == item.Id).Count();
                if (noOfChannel < 1)
                {
                    var channel = new Channel
                    {
                        Name = item.Name,
                        InstituteId = item.Id,
                        ChannelCategoryId = 2,
                        CreatorId = this.ClientId,
                        RecordUpdatedOn = DateTime.Now,
                        Logo = item.Logo,
                        IsDefault = true
                    };

                    _channelRepository.Save(channel);
                }
            }
            return Ok(ToDto(item));
        }

        [HttpGet]
        public override IActionResult Get()
        {
            var creatorId = Request.Query["creatorId"].ToNullableInt();
            var instituteId = Convert.ToInt32(Request.Query["instituteId"]);
            var searchText = Request.Query["searchText"];
            var isOwned = Request.Query["isOwnedByMe"].ToNullableBoolean();
            var isSubscribed = Request.Query["isSubscribedByMe"].ToNullableBoolean();

            var predicate = PredicateBuilder.True<Institute>();

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

            if (instituteId > 0)
            {
                predicate = predicate.And(x => x.Id == instituteId);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                predicate = predicate.And(x => x.Name.Contains(searchText));
            }

            if (isSubscribed.HasValue)
            {
                if (isSubscribed.Value)
                {
                    predicate = predicate.And(x => x.InstituteSubscribes.Any(y => y.UserId == this.ClientId));
                }
                else
                {
                    predicate = predicate.And(x => !x.InstituteSubscribes.Any() || x.InstituteSubscribes.Any(y => y.UserId != this.ClientId));
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

            var institutes = this._instituteRepository.GetBy(predicate).Select(ToDto).ToList();

            foreach (var item in institutes)
            {
                if (item.CreatorId == this.ClientId)
                {
                    item.IsOwner = true;
                }
                else if (item.InstituteSubscribes.Any(x => x.UserId == this.ClientId))
                {
                    item.IsSubscribedByMe = true;
                }
                else if (item.CreatorId != this.ClientId && (item.InstituteSubscribes.Any() || item.InstituteSubscribes.Any(y => y.UserId != this.ClientId)))
                {
                    item.IsNotSubscribedAndOwnedByMe = true;
                }

                SetTotalChannel(item);


            }

            return Ok(institutes);
        }

        private void SetTotalChannel(InstituteDto item)
        {
            var pred = PredicateBuilder.True<Channel>().And(x => x.InstituteId == item.Id);
            var channels = this._channelRepository.GetBy(pred);

            item.TotalChannels = channels != null ? channels.ToList().Count : 0;

            var channelDtos = new List<ChannelDto>();
            foreach (var channel in channels)
            {
                var dto = new ChannelDto();
                dto.Map(channel);
                channelDtos.Add(dto);

                item.TotalChannelUsers += GetTotalChannelUser(dto);
            }

            item.Channels = channelDtos;
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
            var predicate = PredicateBuilder.True<Institute>();
            predicate = predicate.And(x => x.InstituteSubscribes.Any(y => y.UserId == this.ClientId));

            var includeOwnInstitute = Request.Query["includeOwnInstitute"].ToNullableBoolean();
            if (includeOwnInstitute.HasValue && includeOwnInstitute.Value)
            {
                predicate = predicate.Or(x => x.CreatorId == this.ClientId);
            }
            var institutes = this._instituteRepository.GetBy(predicate).Select(ToDto).ToList();

            foreach (var item in institutes.Where(x => x.CreatorId == this.ClientId))
            {
                item.IsOwner = true;
            }

            return Ok(institutes);
        }

        [HttpPost("{instituteId}/adddomains")]
        public IActionResult AddDomains(int instituteId, [FromBody]string[] domains)
        {
            if (domains == null || domains.Count() == 0)
            {
                return BadRequest("Send at least one domain.");
            }
            if (domains.Any(x => !Utility.IsValidDomainName(x)))
            {
                return BadRequest("Cannot add invalid domains.");
            }
            var institute = this._instituteRepository.GetById(instituteId);
            if (institute == null)
            {
                throw new ResourceObjectNotFoundException(typeof(Institute).Name, instituteId);
            }
            if (institute.DomainInstitutes == null)
            {
                institute.DomainInstitutes = new List<DomainInstitute>();
            }
            institute.DomainInstitutes.AddRange(domains.Select(x => new DomainInstitute() { InstituteId = instituteId, DomainName = x }));
            this._instituteRepository.Save(institute);

            return Ok();
        }

        [HttpGet("InstituteCategories")]
        public IActionResult GetInstituteCategories()
        {
            var categories = this._instituteCategoryRepository.Get();
            return Ok(categories);
        }

        [HttpGet("InstituteTypes")]
        public IActionResult GetInstituteTypes()
        {
            var types = this._instituteTypeRepository.Get();
            return Ok(types);
        }

    }
}
