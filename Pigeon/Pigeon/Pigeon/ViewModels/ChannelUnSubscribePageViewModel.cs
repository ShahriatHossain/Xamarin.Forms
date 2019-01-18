using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace Pigeon.ViewModels
{
    public class ChannelUnSubscribePageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IChannelService _channelService;
        public DelegateCommand UnSubscribeCommand { get; set; }

        private ImageSource _orgLogoSource;
        public ImageSource OrgLogoSource
        {
            get { return _orgLogoSource; }
            set { SetProperty(ref _orgLogoSource, value); }
        }

        private string _orgLogo;
        public string OrgLogo
        {
            get { return _orgLogo; }
            set { SetProperty(ref _orgLogo, value); }
        }

        private string _organizationName;
        public string OrganizationName
        {
            get { return _organizationName; }
            set { SetProperty(ref _organizationName, value); }
        }

        private string _organizationShortName;
        public string OrganizationShortName
        {
            get { return _organizationShortName; }
            set { SetProperty(ref _organizationShortName, value); }
        }

        private string _contactPersonName;
        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set { SetProperty(ref _contactPersonName, value); }
        }

        private string _area;
        public string Area
        {
            get { return _area; }
            set { SetProperty(ref _area, value); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        private string _pinCode;
        public string PinCode
        {
            get { return _pinCode; }
            set { SetProperty(ref _pinCode, value); }
        }

        private string _orgDesc;
        public string OrganizationDesc
        {
            get { return _orgDesc; }
            set { SetProperty(ref _orgDesc, value); }
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

        private ChannelApi _oldChannel;
        public ChannelApi OldChannel
        {
            get { return _oldChannel; }
            set { SetProperty(ref _oldChannel, value); }
        }

        private string _serviceSecurePin;
        public string ServiceSecurePin
        {
            get { return _serviceSecurePin; }
            set { SetProperty(ref _serviceSecurePin, value); }
        }


        private ImageResizeHelper _imageResizeHelper;
        private CommonHelper _commonHelper;

        public ChannelUnSubscribePageViewModel(IUserService userService, IChannelService channelService, INavigationService navigationService)
        {
            _userService = userService;
            _channelService = channelService;
            _navigationService = navigationService;
            UnSubscribeCommand = new DelegateCommand(Unsubscribe);
            _imageResizeHelper = new ImageResizeHelper();
            _commonHelper = new CommonHelper();
        }

        private async void Unsubscribe()
        {
            await _commonHelper.ShowLoaderAsync();
            ServiceSecurePin = await _channelService.GetSecurePinCodeAsync(ChannelId);
            var result = await _channelService.UnSubscribeChannelAsync(ChannelId);
            if (result)
            {
                await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);
                _commonHelper.DisplayAlertMessage(string.Empty, "Channel unsubscribed successfully.");

                _commonHelper.UnSubscribeChannelForNotification(ChannelId.ToString());
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CStatus = (string)parameters["cStatus"];
            ChannelId = (int?)parameters["channelId"];
            InitFormWithExistingChannel(CStatus, ChannelId);
        }

        private async void InitFormWithExistingChannel(string cStatus, int? channelId)
        {
            if (channelId != null || channelId > 0)
            {
                await _commonHelper.ShowLoaderAsync();
                OldChannel = await _channelService.GetChannelDetailAsync(channelId, cStatus);
                if (OldChannel != null)
                {
                    OrganizationName = OldChannel.Name;
                }
                await _commonHelper.HideLoaderAsync();
            }
        }
    }
}
