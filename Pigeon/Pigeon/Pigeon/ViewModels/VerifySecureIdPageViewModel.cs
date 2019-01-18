using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Pigeon.ViewModels
{
    public class VerifySecureIdPageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly IChannelService _channelService;
        private readonly INavigationService _navigationService;
        public DelegateCommand VerifyCommand { get; set; }

        private string _securePin;
        public string SecurePin
        {
            get { return _securePin; }
            set { SetProperty(ref _securePin, value); }
        }

        private bool _isHasSecurePin;
        public bool IsHasSecurePin
        {
            get { return _isHasSecurePin; }
            set { SetProperty(ref _isHasSecurePin, value); }
        }
        private int? _channelId;
        public int? ChannelId
        {
            get { return _channelId; }
            set { SetProperty(ref _channelId, value); }
        }
        private string _cStatus;
        public string CStatus
        {
            get { return _cStatus; }
            set { SetProperty(ref _cStatus, value); }
        }
        private string _serviceSecurePin;
        public string ServiceSecurePin
        {
            get { return _serviceSecurePin; }
            set { SetProperty(ref _serviceSecurePin, value); }
        }

        private int _hitCount;
        public int HitCount
        {
            get { return _hitCount; }
            set { SetProperty(ref _hitCount, value); }
        }

        private CommonHelper _commonHelper;

        public VerifySecureIdPageViewModel(IUserService userService, IChannelService channelService, INavigationService navigationService)
        {
            _userService = userService;
            _channelService = channelService;
            _navigationService = navigationService;
            VerifyCommand = new DelegateCommand(Verify);
            _commonHelper = new CommonHelper();
        }

        private async void Verify()
        {
            await _commonHelper.ShowLoaderAsync();
            HitCount = HitCount + 1;
            if (IsHasSecurePin)
            {
                if (HitCount > 10)
                {
                    await _commonHelper.HideLoaderAsync();
                    await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);
                }

                if (ServiceSecurePin != SecurePin)
                {
                    _commonHelper.DisplayAlertMessage(string.Empty, "Enter correct Pin");
                    await _commonHelper.HideLoaderAsync();
                }
                else if (ServiceSecurePin == SecurePin)
                {
                    await _commonHelper.HideLoaderAsync();
                    var result = await _channelService.SubscribeChannelAsync(ChannelId);
                    if (result)
                    {
                        await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);
                        _commonHelper.DisplayAlertMessage(string.Empty, "Channel subscribed successfully.");
                    }
                }
            }
            else
            {
                await _commonHelper.HideLoaderAsync();
                _commonHelper.DisplayAlertMessage(string.Empty, "Enter correct Pin");
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            ChannelId = (int?)parameters["channelId"];
            CStatus = (string)parameters["cStatus"];
            CheckExistingSecurePin();
        }

        private async void CheckExistingSecurePin()
        {
            HitCount = 0;
            IsHasSecurePin = false;
            ServiceSecurePin = await _channelService.GetSecurePinCodeAsync(ChannelId);
            if (ServiceSecurePin != null)
            {
                IsHasSecurePin = true;
            }
        }
    }
}
