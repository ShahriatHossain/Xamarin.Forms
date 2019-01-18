using Pigeon.Helpers;
using Pigeon.Services.Configuration;
using Pigeon.Services.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace Pigeon.ViewModels
{
    public class ChannelSecureIdPageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly IChannelService _channelService;
        private readonly INavigationService _navigationService;
        private readonly IInstituteService _instituteService;

        public DelegateCommand UpdateCommand { get; set; }

        private string _newSecurePin;
        public string NewSecurePin
        {
            get { return _newSecurePin; }
            set { SetProperty(ref _newSecurePin, value); }
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


        private CommonHelper _commonHelper;
        public ChannelSecureIdPageViewModel(
            IUserService userService
            , IChannelService channelService
            , INavigationService navigationService
            , IInstituteService instituteService
            )
        {
            _userService = userService;
            _channelService = channelService;
            _navigationService = navigationService;
            UpdateCommand = new DelegateCommand(Update);
            _commonHelper = new CommonHelper();
            _instituteService = instituteService;
        }

        private async void CheckExistingSecurePin()
        {
            try
            {
                IsHasSecurePin = false;
                ServiceSecurePin = await _channelService.GetSecurePinCodeAsync(ChannelId);
                if (ServiceSecurePin != null)
                {
                    NewSecurePin = ServiceSecurePin;
                    IsHasSecurePin = true;
                }
            }
            catch (Exception ex)
            {
                //
            }


        }

        private async void Update()
        {
            await _commonHelper.ShowLoaderAsync();
            NewSecurePin = string.IsNullOrWhiteSpace(NewSecurePin) ? null : NewSecurePin;
            if (IsHasSecurePin)
            {
                if (ServiceSecurePin == NewSecurePin)
                {
                    await GotoPreviousPage();
                }
                else
                {
                    SaveSecureCode(ChannelId, NewSecurePin);
                }
            }
            else
            {
                SaveSecureCode(ChannelId, NewSecurePin);
            }

            await _commonHelper.HideLoaderAsync();
        }

        private async System.Threading.Tasks.Task GotoPreviousPage()
        {
            var param = new NavigationParameters();
            param.Add("channelId", ChannelId);
            param.Add("cStatus", CStatus);

            InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(ChannelId));

            await _navigationService.NavigateAsync("ChannelDetailViewer", param, false, false);
        }

        private async void SaveSecureCode(int? channelId, string securePin)
        {
            var result = await _channelService.SaveSecurePinCodeAsync(channelId, securePin);
            await GotoPreviousPage();
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

        private string _pageTitle;
        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }
        private async void GetChannelDetailAsync()
        {
            var channel = await _channelService.GetChannelDetailAsync(ChannelId, string.Empty);
            var institute = await _instituteService.GetInstituteDetailAsync(channel.InstituteId);

            PageTitle = string.Format("{0} - {1}", institute.Name, channel.Name);
        }
    }
}
