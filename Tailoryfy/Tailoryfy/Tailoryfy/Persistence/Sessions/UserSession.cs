namespace Tailoryfy.Persistence.Sessions
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

        public void SetUserSession(UserSession userSession)
        {
            _userSession = userSession;
        }

        private bool _isLoggedIn;
        public void SetIsLoggedIn(bool isLoggedIn)
        {
            _isLoggedIn = isLoggedIn;
        }
        public bool IsLoggedIn => _isLoggedIn;


        private string _userNumber;
        public void SetUserNumber(string userNumber)
        {
            _userNumber = userNumber;
        }
        public string UserNumber => _userNumber;


        private string _userId;
        public void SetUserId(string userId)
        {
            _userId = userId;
        }
        public string UserId => _userId;
    }
}
