using Newtonsoft.Json;
using Pigeon.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pigeon.WebApi.Models
{
    public class InstituteDto : BaseDto<Institute>
    {
        public string Name { get; private set; }
        public string Area { get; set; }

        public string District { get; set; }

        public string Division { get; set; }

        public string Description { get; set; }

        public string Logo { get; set; }

        [JsonIgnore]
        public string CreatorId { get; private set; }

        public PigeonUser Creator { get; private set; }

        public bool IsOwner { get; set; }

        public bool IsSubscribedByMe { get; set; } = false;

        public bool IsNotSubscribedAndOwnedByMe { get; set; } = false;

        [JsonProperty("domain_names", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> DomainNames { get; private set; }

        public int InstituteCategoryId { get; set; }

        public InstituteCategory InstituteCategory { get; private set; }

        public int InstituteTypeId { get; set; }

        public InstituteType InstituteType { get; private set; }

        public IList<InstituteSubscribe> InstituteSubscribes { get; set; }

        public string LogoURL
        {
            get
            {
                var url = "#";
                if (!string.IsNullOrEmpty(Logo))
                {
                    url = string.Format("{0}{1}", Constants.AzureBlobUrl, Logo);
                }

                return url;
            }
        }

        public int TotalChannels { get; set; }
        public int TotalChannelUsers { get; set; }
        public IList<ChannelDto> Channels { get; set; }

        public override void Map(Institute model)
        {
            Id = model.Id;
            Name = model.Name;
            Area = model.Area;
            District = model.District;
            Division = model.Division;
            Description = model.Description;
            Logo = model.Logo;
            CreatorId = model.CreatorId;
            Creator = model.Creator;
            InstituteCategoryId = model.InstituteCategoryId;
            InstituteTypeId = model.InstituteTypeId;

            if (model.DomainInstitutes != null)
            {
                this.DomainNames = new List<string>();
                this.DomainNames.AddRange(model.DomainInstitutes.Select(x => x.DomainName));
            }

            InstituteSubscribes = model.InstituteSubscribes;
        }
    }
}
