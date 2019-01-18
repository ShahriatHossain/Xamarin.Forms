using MnMFiber.Common.Helper;
using MnMFiber.Core.Models;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMFiber.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userRepository;
        private readonly Database database;

        public LoginPageViewModel(INavigationService navigationService, IUserService userRepository)
        {
            _navigationService = navigationService;
            _userRepository = userRepository;

            database = new Database("User");
            database.CreateTable<User>();
            database.DeleteAll<User>();

            HotoFiberSession.Current.SetHotoFiberSession(null);

            UserSession.Current.SetUserSession(null);
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand => _loginCommand != null ?
            _loginCommand : (_loginCommand = new DelegateCommand(LoginAsync));

        private async void LoginAsync()
        {
            CheckConnectivityAndInputValidation();

            await CommonHelper.ShowLoaderAsync();

            CustomResponse response = await VerfiryUserLogin();

            if (response != null && response.IsSuccess)
            {
                var user = new User
                {
                    UserName = UserName,
                    AccessToken = string.Empty
                };
                database.SaveItem(user);

                UserSession.Current.SetUserNumber(UserName);

                UserSession.Current.SetUserId(response.UserId.ToString());

                CommonHelper.SubscribeTopicForNotification(response.UserId.ToString());

                await _navigationService.NavigateAsync("ModuleListPage", null, false, false);

                await CommonHelper.HideLoaderAsync();
            }
            else
            {
                await CommonHelper.HideLoaderAsync();
                CommonHelper.DisplayAlertMessage("", "Login failed, Please try again.");
            }
        }

        private async System.Threading.Tasks.Task<CustomResponse> VerfiryUserLogin()
        {
            var account = new UserAccount()
            {
                UserName = UserName,
                Password = Password
            };
            var response = await _userRepository.VerifyLoginAsync(account);
            return response;
        }

        private void CheckConnectivityAndInputValidation()
        {
            if (!CommonHelper.DoIHaveInternet())
            {
                CommonHelper.DisplayAlertMessage("", "Please check your internet connection.");
                return;
            }
            else if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                CommonHelper.DisplayAlertMessage("", "User name and password should not be blank.");
                return;
            }
        }
    }
}
