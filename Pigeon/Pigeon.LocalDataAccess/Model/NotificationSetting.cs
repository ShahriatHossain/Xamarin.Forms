using SQLite;
using System.ComponentModel;

namespace Pigeon.LocalDataAccess.Model
{
    [Table("NotificationSettings")]
  public class NotificationSetting : BindableModel
    {        
        private int _userId;
        [NotNull]
        public int UserId
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
        private bool _messageTone;
        [NotNull]
        public bool MessageTone
        {
            get
            {
                return _messageTone;
            }
            set
            {
                this._messageTone = value;
                OnPropertyChanged(nameof(MessageTone));
            }
        }
        private bool _notificationTone;
        [NotNull]
        public bool NotificationTone
        {
            get
            {
                return _notificationTone;
            }
            set
            {
                this._notificationTone = value;
                OnPropertyChanged(nameof(NotificationTone));
            }
        }
        private bool _vibrate;
        [NotNull]
        public bool Vibrate
        {
            get
            {
                return _vibrate;
            }
            set
            {
                this._vibrate = value;
                OnPropertyChanged(nameof(Vibrate));
            }
        }
        private bool _popupNotification;
        [NotNull]
        public bool PopupNotification
        {
            get
            {
                return _popupNotification;
            }
            set
            {
                this._popupNotification = value;
                OnPropertyChanged(nameof(PopupNotification));
            }
        }        
    }
}
