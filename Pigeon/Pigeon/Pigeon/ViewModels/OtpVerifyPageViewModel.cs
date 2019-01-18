using Pigeon.Helpers;
using Pigeon.LocalDataAccess.Model;
using Pigeon.Services.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace Pigeon.ViewModels
{
    public class OtpVerifyPageViewModel : BindableBase
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        DelegateCommand _verifyCommand;
        public DelegateCommand VerifyCommand => _verifyCommand != null ?
            _verifyCommand : (_verifyCommand = new DelegateCommand(Verify));

        DelegateCommand _resendOtpCommand;
        public DelegateCommand ResendOtpCommand => _resendOtpCommand != null ?
            _resendOtpCommand : (_resendOtpCommand = new DelegateCommand(ResendOtp));

        private string _otpCode;
        public string OtpCode
        {
            get { return _otpCode; }
            set { SetProperty(ref _otpCode, value); }
        }

        private CommonHelper _commonHelper;

        public OtpVerifyPageViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;

            _commonHelper = new CommonHelper();
        }

        private async void ResendOtp()
        {
            await _commonHelper.ShowLoaderAsync();

            var result = await _userService.GetOtpCode(LocalStorageSettings.Email);
            await _commonHelper.HideLoaderAsync();
            if (Equals(result, null)) return;

            if (result.IsOtpSent)
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "One time password has been sent to specified email id.");
            }
        }

        private async void Verify()
        {
            try
            {
                await _commonHelper.ShowLoaderAsync();

                var userDetails = new Services.Model.User()
                {
                    Id = LocalStorageSettings.ServiceUserId,
                    Email = LocalStorageSettings.Email,
                    CountryCode = Convert.ToInt64(LocalStorageSettings.CountryCode),
                    MobileNo = Convert.ToInt64(LocalStorageSettings.MobileNo),
                    Password = OtpCode
                };

                UpdateUserDetail(userDetails);

                userDetails = await _userService.GetToken(LocalStorageSettings.Email, OtpCode);

                if (!string.IsNullOrEmpty(userDetails.Token))
                {
                    this.UpdateUserDetail(userDetails);

                    //UserSession.Current.SetLastRefreshDatetime(System.DateTime.Now.ToString("dd/M/yyyy HH:mm:ss"));

                    await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);

                    await _commonHelper.HideLoaderAsync();

                    await CommonHelper.IsFileStorageSupportedAsync();
                }
                else
                {
                    await _commonHelper.HideLoaderAsync();
                    _commonHelper.DisplayAlertMessage(string.Empty, "Your OTP code is not correct!");
                }
            }
            catch (Exception ex)
            {
                await _commonHelper.HideLoaderAsync();
            }
        }

        private async void UpdateUserDetail(Services.Model.User user)
        {
            LocalStorageSettings.OtpCode = OtpCode;
            LocalStorageSettings.ServiceUserId = user.Id;
            LocalStorageSettings.IsOtpVerified = true;
            LocalStorageSettings.IsOtpSent = false;
            LocalStorageSettings.Token = user.Token;
            LocalStorageSettings.Password = OtpCode;

            await _userService.SaveUserAsync(user, true);
        }
    }
}
