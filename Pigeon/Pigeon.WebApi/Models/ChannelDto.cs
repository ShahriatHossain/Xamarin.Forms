using Newtonsoft.Json;
using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace Pigeon.WebApi.Models
{
    public class ChannelDto : BaseDto<Channel>
    {
        public string Name { get; private set; }

        public string Status { get; private set; }

        [JsonProperty("secure_pin")]
        public string SecurePin { get; private set; }

        [JsonProperty("record_updated_on")]
        public DateTime RecordUpdatedOn { get; private set; }

        [JsonProperty("owner_flag")]
        public bool OwnerFlag { get; set; } = false;

        public int MaxSeqID { get; private set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; private set; }

        [JsonProperty("is_expired")]
        public bool IsExpired { get; private set; }

        [JsonProperty("new_notice_indicator")]
        public string NewNoticeIndicator { get; set; }

        public string Logo { get; set; }

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

        public bool IsDefault { get; set; }

        public bool IsSubscribedByMe { get; set; } = false;

        public bool IsNotSubscribedAndOwnedByMe { get; set; } = false;

        [JsonIgnore]
        public string CreatorId { get; private set; }
        public PigeonUser Creator { get; private set; }

        public int InstituteId { get; set; }

        public InstituteDto Institute { get; private set; }

        public IList<ChannelSubscribe> ChannelSubscribes { get; set; }

        public int ChannelCategoryId { get; set; }

        public int TotalChannelUsers { get; set; }

        public override void Map(Channel model)
        {
            MapPrimaryProperties(model);
        }

        public void MapPrimaryProperties(Channel model)
        {
            Id = model.Id;
            Name = model.Name;
            Status = model.Status;
            SecurePin = model.SecurePin;
            RecordUpdatedOn = model.RecordUpdatedOn;
            OwnerFlag = model.OwnerFlag;
            MaxSeqID = model.MaxSeqID;
            IsVerified = model.IsVerified;
            IsExpired = model.IsExpired;
            NewNoticeIndicator = model.NewNoticeIndicator;
            CreatorId = model.CreatorId;
            Creator = model.Creator;
            Logo = model.Logo;
            IsDefault = model.IsDefault;
            InstituteId = model.InstituteId;
            Institute = new InstituteDto();
            Institute.Map(model.Institute);
            ChannelCategoryId = model.ChannelCategoryId;

            ChannelSubscribes = model.ChannelSubscribes;
        }

        public void MapNavigationProperties(Channel model)
        {
            this.Institute = new InstituteDto();
            //this.Institute.Map(model.Institute);
        }

        public void Map(Channel model, bool includeNavigation)
        {
            MapPrimaryProperties(model);
            if (includeNavigation)
            {
                MapNavigationProperties(model);
            }
        }
    }
}
