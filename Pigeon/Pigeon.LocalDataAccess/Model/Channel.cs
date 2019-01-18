using SQLite;
using System.ComponentModel;

namespace Pigeon.LocalDataAccess.Model
{

    [Table("Channels")]
    public class Channel : BindableModel
    {        
        private string _userId;
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                this._userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        private int _serviceChannelId;
        public int ServiceChannelId
        {
            get
            {
                return _serviceChannelId;
            }
            set
            {
                this._serviceChannelId = value;
                OnPropertyChanged(nameof(ServiceChannelId));
            }
        }
        private string _orgName;
        public string OrgName
        {
            get
            {
                return _orgName;
            }
            set
            {
                this._orgName = value;
                OnPropertyChanged(nameof(OrgName));
            }
        }
        private string _orgShortName;
        public string OrgShortName
        {
            get
            {
                return _orgShortName;
            }
            set
            {
                this._orgShortName = value;
                OnPropertyChanged(nameof(OrgShortName));
            }
        }

        private string _orgContactPersonName;
        public string OrgContactPersonName
        {
            get
            {
                return _orgContactPersonName;
            }
            set
            {
                this._orgContactPersonName = value;
                OnPropertyChanged(nameof(OrgContactPersonName));
            }
        }

        private string _orgArea;
        public string OrgArea
        {
            get
            {
                return _orgArea;
            }
            set
            {
                this._orgArea = value;
                OnPropertyChanged(nameof(OrgArea));
            }
        }

        private string _orgCityName;
        public string OrgCityName
        {
            get
            {
                return _orgCityName;
            }
            set
            {
                this._orgCityName = value;
                OnPropertyChanged(nameof(OrgCityName));
            }
        }

        private string _orgState;
        public string OrgState
        {
            get
            {
                return _orgState;
            }
            set
            {
                this._orgState = value;
                OnPropertyChanged(nameof(OrgState));
            }
        }

        private string _orgCountry;
        public string OrgCountry
        {
            get
            {
                return _orgCountry;
            }
            set
            {
                this._orgCountry = value;
                OnPropertyChanged(nameof(OrgCountry));
            }
        }

        private string _orgPincode;
        public string OrgPincode
        {
            get
            {
                return _orgPincode;
            }
            set
            {
                this._orgPincode = value;
                OnPropertyChanged(nameof(OrgPincode));
            }
        }

        private string _orgLogo;
        public string OrgLogo
        {
            get
            {
                return _orgLogo;
            }
            set
            {
                this._orgLogo = value;
                OnPropertyChanged(nameof(OrgLogo));
            }
        }

        private string _orgDesc;
        public string OrgDesc
        {
            get
            {
                return _orgDesc;
            }
            set
            {
                this._orgDesc = value;
                OnPropertyChanged(nameof(OrgDesc));
            }
        }

        private string _securePin;
        public string SecurePin
        {
            get
            {
                return _securePin;
            }
            set
            {
                this._securePin = value;
                OnPropertyChanged(nameof(SecurePin));
            }
        }

        private string _recordUpdatedOn;
        public string RecordUpdatedOn
        {
            get
            {
                return _recordUpdatedOn;
            }
            set
            {
                this._recordUpdatedOn = value;
                OnPropertyChanged(nameof(RecordUpdatedOn));
            }
        }

        private string _orgLogoFileType;
        public string OrgLogoFileType
        {
            get
            {
                return _orgLogoFileType;
            }
            set
            {
                this._orgLogoFileType = value;
                OnPropertyChanged(nameof(OrgLogoFileType));
            }
        }

        private string _orgFileDisplayName;
        public string OrgFileDisplayName
        {
            get
            {
                return _orgFileDisplayName;
            }
            set
            {
                this._orgFileDisplayName = value;
                OnPropertyChanged(nameof(OrgFileDisplayName));
            }
        }

        private string _ownerFlag;
        public string OwnerFlag
        {
            get
            {
                return _ownerFlag;
            }
            set
            {
                this._ownerFlag = value;
                OnPropertyChanged(nameof(OwnerFlag));
            }
        }

        private string _verified;
        public string Verified
        {
            get
            {
                return _verified;
            }
            set
            {
                this._verified = value;
                OnPropertyChanged(nameof(Verified));
            }
        }

        private int _maxSeqID;
        public int MaxSeqID
        {
            get
            {
                return _maxSeqID;
            }
            set
            {
                this._maxSeqID = value;
                OnPropertyChanged(nameof(MaxSeqID));
            }
        }
    }
}
