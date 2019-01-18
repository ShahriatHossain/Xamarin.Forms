using Pigeon.Services.Implementation;

namespace Pigeon.Services.Configuration
{
    public class UserSession
    {
        private PushNotificationService _pushNotificationService;
        private UserSession()
        {
            _pushNotificationService = new PushNotificationService();
        }

        private static UserSession _userSession;
        private static object locker = new object();

        public static UserSession Current
        {
            get
            {
                if (_userSession == null)
                {
                    lock (locker)
                    {
                        if (_userSession == null)
                        {
                            _userSession = new UserSession();
                        }
                    }
                }
                return _userSession;
            }
        }

        private string _serviceUserId;

        public void SetServiceUserId(string serviceUserId)
        {
            _serviceUserId = serviceUserId;
        }

        private string _lastRefreshDatetime;

        public void SetLastRefreshDatetime(string lastRefreshDatetime)
        {
            _lastRefreshDatetime = lastRefreshDatetime;
        }

        public string LastRefreshDatetime => _lastRefreshDatetime;

        public string ServiceUserId => HasUser ? _serviceUserId : string.Empty;

        public bool HasUser => _serviceUserId != null;




        public PushNotificationService NotificationService => _pushNotificationService;


    }
}
