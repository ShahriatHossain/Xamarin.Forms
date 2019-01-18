using Newtonsoft.Json;
using Pigeon.Services.Configuration;
using System;
using System.Collections.ObjectModel;

namespace Pigeon.Services.Model
{
    public class ChannelApi
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "secure_pin")]
        public string SecurePin { get; set; }
        [JsonProperty(PropertyName = "record_updated_on")]
        public string RecordUpdatedOn { get; set; }
        [JsonProperty(PropertyName = "owner_flag")]
        public bool OwnerFlag { get; set; }
        [JsonProperty(PropertyName = "maxSeqID")]
        public string MaxSeqID { get; set; }
        [JsonProperty(PropertyName = "is_verified")]
        public bool Isverified { get; set; }
        [JsonProperty(PropertyName = "is_expired")]
        public bool IsExpired { get; set; }
        [JsonProperty(PropertyName = "new_notice_indicator")]
        public string NewNoticeIndicator { get; set; }
        public int ChannelCategoryId { get; set; }
        public string Logo { get; set; }

        private Uri _logoUri;
        public Uri LogoUri
        {
            get
            {
                if (Logo != null)
                {
                    _logoUri = new Uri(GlobalVariables.AzureBlobUrl + Logo);
                }
                return _logoUri;
            }
        }

        public int InstituteId { get; set; }
        public bool IsDefault { get; set; }

        private bool _isPrivate;
        public bool IsPrivate
        {
            get
            {
                if (!IsDefault)
                {
                    if ((ChannelCategoryId == (int)CommonEnums.ChannelCategory.Private) || SecurePin != null)
                    {
                        _isPrivate = true;
                    }
                    else
                    {
                        _isPrivate = false;
                    }
                }

                return _isPrivate;
            }
        }

        public bool IsSubscribedByMe { get; set; }

        public bool IsNotSubscribedAndOwnedByMe { get; set; }

        public bool IsVisibleSubscribeBtn2
        {
            get
            {
                if (OwnerFlag || IsSubscribedByMe || IsNotSubscribedAndOwnedByMe)
                    return true;

                return false;
            }
        }
        public string SubscribeBtnIcon2
        {
            get
            {
                if (OwnerFlag)
                    return "tick2020.png";
                else if (IsSubscribedByMe)
                    return "tick2020.png";
                else if (IsNotSubscribedAndOwnedByMe)
                    return string.Empty;

                return string.Empty;
            }
        }
        public string SubscribeBtnText2
        {
            get
            {
                if (OwnerFlag)
                    return "Owned";
                else if (IsSubscribedByMe)
                    return "Subscribed";
                else if (IsNotSubscribedAndOwnedByMe)
                    return "Subscribe";

                return "Subscribe";
            }
        }
        public string SubscribeBtnTextColor2
        {
            get
            {
                if (OwnerFlag)
                    return "Gray";
                else if (IsSubscribedByMe)
                    return "Gray";
                else if (IsNotSubscribedAndOwnedByMe)
                    return "White";

                return "White";
            }
        }

        public string SubscribeBtnColor2
        {
            get
            {
                if (OwnerFlag)
                    return "#E3E2E2";
                else if (IsSubscribedByMe)
                    return "#E3E2E2";
                else if (IsNotSubscribedAndOwnedByMe)
                    return "#FF5959";

                return "#FF5959";
            }
        }

        private string _channelId;
        public string ChannelId
        {
            get
            {
                _channelId = Convert.ToString(Id);
                return _channelId;
            }
        }

        public ObservableCollection<ChannelSubscribe> ChannelSubscribes { get; set; }

        public int NumberOfSubscribers
        {
            get
            {
                if (ChannelSubscribes is null)
                    return 0;

                return ChannelSubscribes.Count;
            }
        }

        public bool HasNewNoticeCreated
        {
            get
            {
                if (string.IsNullOrEmpty(NewNoticeIndicator))
                    return false;

                return true;
            }
        }
    }
}
