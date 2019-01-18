using Pigeon.Helpers;
using Pigeon.LocalDataAccess.Model;
using Pigeon.Services.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Pigeon.ViewModels
{
    public class MobileNoChangePageViewModel : BindableBase
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        public DelegateCommand UpdateCommand { get; set; }

        private string _oldCountryCode;
        public string OldCountryCode
        {
            get { return _oldCountryCode; }
            set { SetProperty(ref _oldCountryCode, value); }
        }

        private string _oldMobileNo;
        public string OldMobileNo
        {
            get { return _oldMobileNo; }
            set { SetProperty(ref _oldMobileNo, value); }
        }

        private string _newCountryCode;
        public string NewCountryCode
        {
            get { return _newCountryCode; }
            set { SetProperty(ref _newCountryCode, value); }
        }

        private string _newMobileNo;
        public string NewMobileNo
        {
            get { return _newMobileNo; }
            set { SetProperty(ref _newMobileNo, value); }
        }

        private CommonHelper _commonHelper;

        public MobileNoChangePageViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            UpdateCommand = new DelegateCommand(Update);
            _commonHelper = new CommonHelper();
            InitForm();
        }

        private async void InitForm()
        {

            await _commonHelper.HideLoaderAsync();

            OldCountryCode = LocalStorageSettings.CountryCode;
            OldMobileNo = LocalStorageSettings.MobileNo;
        }

        private async void Update()
        {
            if (!_commonHelper.HasNetwork())
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "No network found!");
                return;
            }

            if ((OldCountryCode != NewCountryCode) || (OldMobileNo != NewMobileNo))
            {
                await _commonHelper.ShowLoaderAsync();

                var result = await _userService.GetOtpCode(LocalStorageSettings.Email);
                await _commonHelper.HideLoaderAsync();
                if (Equals(result, null)) return;

                if (result.IsOtpSent)
                {
                    await _navigationService.NavigateAsync("OtpVerifyPage", null, false, false);

                    LocalStorageSettings.CountryCode = NewCountryCode;
                    LocalStorageSettings.MobileNo = NewMobileNo;
                    LocalStorageSettings.OtpCode = result.OtpCode;
                    LocalStorageSettings.IsOtpVerified = false;
                    LocalStorageSettings.IsOtpSent = true;
                    _commonHelper.DisplayAlertMessage(string.Empty, "One time password has been sent to specified email id.");
                }

            }
            else
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "The new mobile number should be different from old number.");
            }
        }
    }
}
