using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.LocalDataAccess.Model
{
    [Table("ChannelDetails")]
    public class ChannelDetail : BindableModel
    {
        private int _channelId;
        public int ChannelId
        {
            get
            {
                return _channelId;
            }
            set
            {
                this._channelId = value;
                OnPropertyChanged(nameof(ChannelId));
            }
        }

        private int _orgDocType;
        public int OrgDocType
        {
            get
            {
                return _orgDocType;
            }
            set
            {
                this._orgDocType = value;
                OnPropertyChanged(nameof(OrgDocType));
            }
        }

        private string _orgDoc;
        public string OrgDoc
        {
            get
            {
                return _orgDoc;
            }
            set
            {
                this._orgDoc = value;
                OnPropertyChanged(nameof(OrgDoc));
            }
        }

        private string _orgFileType;
        public string OrgFileType
        {
            get
            {
                return _orgFileType;
            }
            set
            {
                this._orgFileType = value;
                OnPropertyChanged(nameof(_orgFileType));
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
                OnPropertyChanged(nameof(_orgFileDisplayName));
            }
        }
    }
}
