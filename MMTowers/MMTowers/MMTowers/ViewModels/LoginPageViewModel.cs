using MMTowers.Helper;
using MMTowers.Service.Common;
using MMTowers.Service.DataAccess;
using MMTowers.Service.Interface;
using MMTowers.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMTowers.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand => _loginCommand != null ?
            _loginCommand : (_loginCommand = new DelegateCommand(VerifyLoginAsync));

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

        private async void VerifyLoginAsync()
        {
            if (!CommonHelper.HasInternetConnection())
            {
                CommonHelper.DisplayAlertMessage("", "Please check your internet connection.");
                return;
            }

            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
            {
                CommonHelper.DisplayAlertMessage("", "User name and password should not be blank.");
                return;
            }

            await CommonHelper.ShowLoaderAsync();

            var account = new Account()
            {
                UserName = UserName,
                Password = Password
            };
            var response = await _accountService.VerifyLoginAsync(account);
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    UserSession.Current.SetUserName(UserName);

                    var record = new User
                    {
                        UserName = UserName,
                        AccessToken = string.Empty
                    };
                    DatabaseHelper.SaveUser(record);

                    await _navigationService.NavigateAsync("TowerListPage", null, false, false);

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

        private readonly INavigationService _navigationService;

        private readonly IAccountService _accountService;
        
        public LoginPageViewModel(INavigationService navigationService, IAccountService accountService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            DatabaseHelper.DeleteAllUsers();
        }
    }
}
