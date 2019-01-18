using Pigeon.Helpers;
using Pigeon.LocalDataAccess.Model;
using Pigeon.Services.Interface;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace Pigeon.ViewModels
{
    public class UserRegistrationPageViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(Save));

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _countryCode;
        public string CountryCode
        {
            get { return _countryCode; }
            set { SetProperty(ref _countryCode, value); }
        }

        private string _mobileNo;
        public string MobileNo
        {
            get { return _mobileNo; }
            set { SetProperty(ref _mobileNo, value); }
        }

        private CommonHelper _commonHelper;

        public UserRegistrationPageViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;

            _commonHelper = new CommonHelper();
        }


        private async void Save()
        {
            try
            {
                if (!_commonHelper.HasNetwork())
                {
                    _commonHelper.DisplayAlertMessage(string.Empty, "No network found!");
                    return;
                }

                await _commonHelper.ShowLoaderAsync();

                Email = Email.ToLower();
                var result = await _userService.GetOtpCode(Email);

                await _commonHelper.HideLoaderAsync();
                if (Equals(result, null)) return;

                if (result.IsOtpSent)
                {
                    await _navigationService.NavigateAsync("OtpVerifyPage", null, false, false);

                    _commonHelper.DisplayAlertMessage(string.Empty, "One time password has been sent to specified email id.");

                    UpdateLocalUserData(result.IsOtpSent);
                }
            }
            catch (Exception ex)
            {
                //
                await _commonHelper.HideLoaderAsync();
            }
        }

        private void UpdateLocalUserData(bool isOtpSent)
        {
            LocalStorageSettings.ClearEverything();

            LocalStorageSettings.IsOtpSent = isOtpSent;
            LocalStorageSettings.Email = Email;
            LocalStorageSettings.CountryCode = CountryCode;
            LocalStorageSettings.MobileNo = MobileNo;
        }
    }
}
