using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace Pigeon.ViewModels
{
    public class ChannelSubscribePageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IChannelService _channelService;
        private readonly IInstituteService _instituteService;
        public DelegateCommand SubscribeCommand { get; set; }

        private Uri _orgLogoSource;
        public Uri OrgLogoSource
        {
            get { return _orgLogoSource; }
            set { SetProperty(ref _orgLogoSource, value); }
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

        public ChannelSubscribePageViewModel
            (
            IUserService userService,
            IChannelService channelService,
            INavigationService navigationService,
            IInstituteService instituteService
            )
        {
            _userService = userService;
            _channelService = channelService;
            _navigationService = navigationService;
            SubscribeCommand = new DelegateCommand(Subscribe);
            _imageResizeHelper = new ImageResizeHelper();
            _commonHelper = new CommonHelper();
            _instituteService = instituteService;
        }

        private async void Subscribe()
        {
            await _commonHelper.ShowLoaderAsync();
            ServiceSecurePin = await _channelService.GetSecurePinCodeAsync(ChannelId);
            if (!string.IsNullOrWhiteSpace(ServiceSecurePin))
            {
                await _commonHelper.HideLoaderAsync();
                var param = new NavigationParameters();
                param.Add("channelId", ChannelId);
                param.Add("cStatus", CStatus);
                await _navigationService.NavigateAsync("VerifySecureIdPage", param, false, false);

            }
            else
            {
                var result = await _channelService.SubscribeChannelAsync(ChannelId);

                await _commonHelper.HideLoaderAsync();

                if (result)
                {
                    await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);
                    _commonHelper.DisplayAlertMessage(string.Empty, "Channel subscribed successfully.");

                    _commonHelper.SubscribeChannelForNotification(ChannelId.ToString());
                }
            }

        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CStatus = (string)parameters["cStatus"];
            ChannelId = (int?)parameters["channelId"];
            InitFormWithExistingChannel(CStatus, ChannelId);
        }

        private string _instituteName;
        public string InstituteName
        {
            get { return _instituteName; }
            set { SetProperty(ref _instituteName, value); }
        }

        private string _area;
        public string Area
        {
            get { return _area; }
            set { SetProperty(ref _area, value); }
        }

        private string _district;
        public string District
        {
            get { return _district; }
            set { SetProperty(ref _district, value); }
        }

        private string _division;
        public string Division
        {
            get { return _division; }
            set { SetProperty(ref _division, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _channelName;
        public string ChannelName
        {
            get { return _channelName; }
            set { SetProperty(ref _channelName, value); }
        }

        private async void InitFormWithExistingChannel(string cStatus, int? channelId)
        {
            if (channelId != null || channelId > 0)
            {
                await _commonHelper.ShowLoaderAsync();
                OldChannel = await _channelService.GetChannelDetailAsync(channelId, cStatus);
                OrgLogoSource = OldChannel.LogoUri;
                if (OldChannel != null)
                {
                    ChannelName = OldChannel.Name;

                    if (OldChannel.InstituteId > 0)
                    {
                        var instituteDetails = await _instituteService.GetInstituteDetailAsync(OldChannel.InstituteId);

                        if (instituteDetails != null)
                        {
                            Area = instituteDetails.Area;
                            District = instituteDetails.District;
                            Division = instituteDetails.Division;
                            Description = instituteDetails.Description;
                            InstituteName = instituteDetails.Name;
                        }
                    }
                }
                await _commonHelper.HideLoaderAsync();
            }
        }
    }
}
