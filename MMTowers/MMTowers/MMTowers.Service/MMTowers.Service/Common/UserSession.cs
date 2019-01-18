using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTowers.Service.Common
{
    public class UserSession
    {
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

        private string _accessToken;
        public void SetAccessToken(string accessToken)
        {
            _accessToken = accessToken;
        }
        public string AccessToken => _accessToken;

        private string _userName;
        public void SetUserName(string userName)
        {
            _userName = userName;
        }
        public string UserName => _userName;
    }
}
