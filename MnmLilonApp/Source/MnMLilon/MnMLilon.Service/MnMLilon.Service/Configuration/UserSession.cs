using MnMLilon.Service.Model;
using Plugin.FilePicker.Abstractions;
using Plugin.Media.Abstractions;
using System.Collections.ObjectModel;
using System.IO;

namespace MnMLilon.Service.Configuration
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


        private string _userId;
        public void SetUserId(string userId)
        {
            _userId = userId;
        }
        public string UserId => _userId;
    }
}
