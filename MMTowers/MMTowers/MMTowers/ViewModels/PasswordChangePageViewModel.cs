using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using MMTowers.Helper;
using MMTowers.Service.DataAccess;
using MMTowers.Service.Interface;
using MMTowers.Service.Model;
using MMTowers.Service.Common;

namespace MMTowers.ViewModels
{
    public class PasswordChangePageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        public PasswordChangePageViewModel(INavigationService navigationService, IAccountService accountService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
        }

        DelegateCommand _changePasswordCommand;
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand != null ?
            _changePasswordCommand : (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));

        private string _oldPassword;
        public string OldPassword
        {
            get { return _oldPassword; }
            set { SetProperty(ref _oldPassword, value); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set { SetProperty(ref _newPassword, value); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        private async void ChangePasswordAsync()
        {
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                CommonHelper.DisplayAlertMessage("", "All those fields required.");
            }
            else if (!NewPassword.Equals(ConfirmPassword))
            {
                CommonHelper.DisplayAlertMessage("", "Confirm password doesn't match.");
            }
            else
            {
                var account = new Account()
                {
                    UserName = UserSession.Current.UserName,
                    Password = NewPassword,
                    ConfirmPassword = ConfirmPassword,
                    OldPassword = OldPassword
                };

                var response = await _accountService.UpdatePasswordAsync(account);

                DatabaseHelper.DeleteAllUsers();

                CommonHelper.DisplayAlertMessage("", "Successfully updated.");

                await _navigationService.NavigateAsync("LoginPage", null, false, false);
            }
        }
    }
}
