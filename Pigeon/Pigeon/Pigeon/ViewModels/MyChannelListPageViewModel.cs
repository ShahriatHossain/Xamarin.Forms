using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace Pigeon.ViewModels
{
    public class MyChannelListPageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly IChannelService _channelService;
        private ObservableCollection<ChannelApi> _approvedChannels;
        public ObservableCollection<ChannelApi> ApprovedChannels
        {
            get { return _approvedChannels; }
            set { SetProperty(ref _approvedChannels, value); }
        }
        private ObservableCollection<ChannelApi> _unApprovedChannels;
        public ObservableCollection<ChannelApi> UnApprovedChannels
        {
            get { return _unApprovedChannels; }
            set { SetProperty(ref _unApprovedChannels, value); }
        }
        private readonly INavigationService _navigationService;
        public DelegateCommand ChannelCreateCommand { get; set; }
        public DelegateCommand<ChannelApi> ChannelDetailCommand { get; set; }
        public DelegateCommand LoadApprovedChannelCommand { get; set; }
        public DelegateCommand LoadUnApprovedChannelCommand { get; set; }
        public DelegateCommand GotoParentCommand { get; set; }

        private string _btnApprovedBgColor;
        public string BtnApprovedBgColor
        {
            get { return _btnApprovedBgColor; }
            set { SetProperty(ref _btnApprovedBgColor, value); }
        }

        private string _btnUnApprovedBgColor;
        public string BtnUnApprovedBgColor
        {
            get { return _btnUnApprovedBgColor; }
            set { SetProperty(ref _btnUnApprovedBgColor, value); }
        }

        private bool _isShowDefaultText;
        public bool IsShowDefaultText
        {
            get { return _isShowDefaultText; }
            set { SetProperty(ref _isShowDefaultText, value); }
        }

        private CommonHelper _commonHelper;

        public MyChannelListPageViewModel(IUserService userService, IChannelService channelService, INavigationService navigationService)
        {
            _userService = userService;
            _channelService = channelService;
            _navigationService = navigationService;
            _commonHelper = new CommonHelper();
            ChannelCreateCommand = new DelegateCommand(NavigateToChannelCreate);
            ChannelDetailCommand = new DelegateCommand<ChannelApi>(NavigateToChannelDetail);
            LoadApprovedChannelCommand = new DelegateCommand(LoadApprovedChannel);
            LoadUnApprovedChannelCommand = new DelegateCommand(LoadUnApprovedChannel);
            GotoParentCommand = new DelegateCommand(GotoParent);
            InitializeChannels();

        }
        private async void GotoParent()
        {
            await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);
        }

        private string _noRecordFound;
        public string NoRecordFound
        {
            get { return _noRecordFound; }
            set { SetProperty(ref _noRecordFound, value); }
        }

        private void CheckNetworkAndChannelAvailability()
        {
            if (!_commonHelper.HasNetwork())
            {
                IsShowDefaultText = true;
                NoRecordFound = "No network found.";
                return;
            }
            else if (ApprovedChannels == null)
            {
                IsShowDefaultText = true;
                NoRecordFound = "No record found.";
                return;
            }
        }

        private async void PopulateChannelList(string channelType)
        {
            if (_commonHelper.HasNetwork())
            {
                ApprovedChannels = null;
                await _commonHelper.ShowLoaderAsync();
                ApprovedChannels = await _channelService.GetChannelByUserAsync(
                    1, string.Empty);
                await _commonHelper.HideLoaderAsync();
                if (ApprovedChannels != null)
                    IsShowDefaultText = false;
            }
        }

        private async void InitializeChannels()
        {
            try
            {
                this.PopulateChannelList("A");
                BtnApprovedBgColor = "#0a4485";
                BtnUnApprovedBgColor = "#227fcc";
                this.CheckNetworkAndChannelAvailability();
            }
            catch (Exception ex)
            {
                await _commonHelper.HideLoaderAsync();
            }
        }

        private async void LoadApprovedChannel()
        {
            try
            {
                this.PopulateChannelList("A");
                BtnApprovedBgColor = "#0a4485";
                BtnUnApprovedBgColor = "#227fcc";
                this.CheckNetworkAndChannelAvailability();
            }
            catch (Exception ex)
            {
                await _commonHelper.HideLoaderAsync();
            }
        }

        private async void LoadUnApprovedChannel()
        {
            try
            {
                this.PopulateChannelList("U");
                BtnUnApprovedBgColor = "#0a4485";
                BtnApprovedBgColor = "#227fcc";
                this.CheckNetworkAndChannelAvailability();
            }
            catch (Exception ex)
            {
                await _commonHelper.HideLoaderAsync();
            }
        }
        private async void NavigateToChannelCreate()
        {
            var param = new NavigationParameters();
            param.Add("channelId", null);
            param.Add("cStatus", "");
            await _navigationService.NavigateAsync("ChannelCreatePage", param, false, false);
        }
        private async void NavigateToChannelDetail(ChannelApi paramChannel)
        {
            if (paramChannel == null)
            {
                return;
            }
            var param = new NavigationParameters();
            param.Add("channelId", paramChannel.Id);
            param.Add("cStatus", paramChannel.Status);
            await _navigationService.NavigateAsync("ChannelOptionPage", param, false, false);
        }

        private ChannelApi _selectedChannelApi;
        public ChannelApi SelectedChannelApi
        {
            get { return _selectedChannelApi; }
            set { SetProperty(ref _selectedChannelApi, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            this.SelectedChannelApi = null;
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
