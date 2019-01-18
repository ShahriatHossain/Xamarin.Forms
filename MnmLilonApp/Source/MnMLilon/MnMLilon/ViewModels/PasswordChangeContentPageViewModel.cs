using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.DataAccess.Implementation;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMLilon.ViewModels
{
    public class PasswordChangeContentPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        public PasswordChangeContentPageViewModel(INavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
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
                var account = new UserAccount()
                {
                    UserName = UserSession.Current.UserName,
                    Password = NewPassword,
                    ConfirmPassword = ConfirmPassword,
                    OldPassword = OldPassword
                };

                var response = await _userService.UpdatePasswordAsync(account);

                UserHelper.DeleteAllUsers();

                CommonHelper.DisplayAlertMessage("", "Successfully updated.");

                await _navigationService.NavigateAsync("LoginContentPage", null, false, false);
            }
        }
    }
}
