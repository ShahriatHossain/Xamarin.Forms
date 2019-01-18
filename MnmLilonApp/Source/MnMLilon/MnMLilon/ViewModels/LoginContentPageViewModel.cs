using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.DataAccess.Implementation;
using MnMLilon.Service.DataAccess.Model;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace MnMLilon.ViewModels
{
    public class LoginContentPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        private readonly IUserService _userService;

        public LoginContentPageViewModel(INavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
            UserHelper.DeleteAllUsers();
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
            _loginCommand : (_loginCommand = new DelegateCommand(VerifyLoginAsync));

        private async void VerifyLoginAsync()
        {
            try
            {
                if (!CommonHelper.HasInternetConnection())
                {
                    CommonHelper.DisplayAlertMessage("", "Please check your internet connection.");
                    return;
                }

                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    CommonHelper.DisplayAlertMessage("", "User name and password should not be blank.");
                    return;
                }

                await CommonHelper.ShowLoaderAsync();

                var account = new UserAccount()
                {
                    UserName = UserName,
                    Password = Password
                };
                var response = await _userService.VerifyLoginAsync(account);

                if (response != null)
                {
                    if (response.IsSuccess)
                    {
                        
                        UserSession.Current.SetUserName(UserName);

                        CommonHelper.SubscribeTopicForNotification(response.UserId.ToString());

                        UserSession.Current.SetUserId(response.UserId.ToString());

                        var record = new User
                        {
                            UserName = UserName,
                            AccessToken = string.Empty
                        };
                        UserHelper.SaveUser(record);

                        await _navigationService.NavigateAsync("CategoryContentPage", null, false, false);

                        await CommonHelper.HideLoaderAsync();
                    }
                    else
                    {
                        await CommonHelper.HideLoaderAsync();

                        CommonHelper.DisplayAlertMessage("", "Please try again.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();
            }
            
        }
    }
}
