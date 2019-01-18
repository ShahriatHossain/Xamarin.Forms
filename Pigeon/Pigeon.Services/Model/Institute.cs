using Pigeon.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pigeon.Services.Model
{
    public class Institute : BaseModel
    {
        public string Name { get; set; }

        public string Area { get; set; }

        public string District { get; set; }

        public string Division { get; set; }

        public string Description { get; set; }

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

        public string CreatorId { get; set; }

        public DateTime CreationTime { get; set; }

        public int PricingTypeId { get; set; }

        public int InstituteCategoryId { get; set; }

        public int InstituteTypeId { get; set; }

        private string _instituteId;
        public string InstituteId
        {
            get
            {
                _instituteId = Convert.ToString(Id);
                return _instituteId;
            }
        }

        public bool IsOwner { get; set; }

        public bool IsSubscribedByMe { get; set; }

        public bool IsNotSubscribedAndOwnedByMe { get; set; }

        public ObservableCollection<InstituteSubscribe> InstituteSubscribes { get; set; }

        public int NumberOfSubscribers
        {
            get
            {
                if (InstituteSubscribes is null)
                    return 0;

                return InstituteSubscribes.Count;
            }
        }


        public bool IsVisibleSubscribeBtn
        {
            get
            {
                if (IsOwner || IsSubscribedByMe || IsNotSubscribedAndOwnedByMe)
                    return true;

                return false;
            }
        }
        public string SubscribeBtnIcon
        {
            get
            {
                if (IsOwner)
                    return "tick2020.png";
                else if (IsSubscribedByMe)
                    return "tick2020.png";
                else if (IsNotSubscribedAndOwnedByMe)
                    return string.Empty;

                return string.Empty;
            }
        }
        public string SubscribeBtnText
        {
            get
            {
                if (IsOwner)
                    return "Owned";
                else if (IsSubscribedByMe)
                    return "Subscribed";
                else if (IsNotSubscribedAndOwnedByMe)
                    return "Subscribe";

                return "Subscribe";
            }
        }
        public string SubscribeBtnTextColor
        {
            get
            {
                if (IsOwner)
                    return "Gray";
                else if (IsSubscribedByMe)
                    return "Gray";
                else if (IsNotSubscribedAndOwnedByMe)
                    return "White";

                return "White";
            }
        }

        public string SubscribeBtnColor
        {
            get
            {
                if (IsOwner)
                    return "#E3E2E2";
                else if (IsSubscribedByMe)
                    return "#E3E2E2";
                else if (IsNotSubscribedAndOwnedByMe)
                    return "#FF5959";

                return "#FF5959";
            }
        }

        public string InstituteAddress
        {
            get
            {
                List<string> address = new List<string>
                {
                      Convert.ToString(Area)
                    , Convert.ToString(District)
                    , Convert.ToString(Division)
                };

                string d = ",";
                return address.Aggregate(string.Empty, (x, y) => x == string.Empty ? y : x + d + y);
            }
        }
    }
}
