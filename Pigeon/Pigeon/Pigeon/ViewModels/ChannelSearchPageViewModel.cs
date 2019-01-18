using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.ViewModels
{
    public class ChannelSearchPageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly IChannelService _channelService;
        private ObservableCollection<ChannelApi> _channels;
        public ObservableCollection<ChannelApi> Channels
        {
            get { return _channels; }
            set { SetProperty(ref _channels, value); }
        }
        private readonly INavigationService _navigationService;
        public DelegateCommand ChannelSearchCommand { get; set; }
        public DelegateCommand<ChannelApi> ChannelDetailCommand { get; set; }

        private bool _isShowDefaultText;
        public bool IsShowDefaultText
        {
            get { return _isShowDefaultText; }
            set { SetProperty(ref _isShowDefaultText, value); }
        }
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private string _noRecordFound;
        public string NoRecordFound
        {
            get { return _noRecordFound; }
            set { SetProperty(ref _noRecordFound, value); }
        }
        private CommonHelper _commonHelper;
        public ChannelSearchPageViewModel(IUserService userService, IChannelService channelService, INavigationService navigationService)
        {
            _userService = userService;
            _channelService = channelService;
            _navigationService = navigationService;
            _commonHelper = new CommonHelper();
            ChannelSearchCommand = new DelegateCommand(InitializeChannels);
            ChannelDetailCommand = new DelegateCommand<ChannelApi>(NavigateToChannelDetail);

            InitializeChannels();
        }

        private async void InitializeChannels()
        {
            await SearchChannel();
        }

        private async Task SearchChannel()
        {
            try
            {
                IsShowDefaultText = false;
                HasNetworkAvailable();
                await _commonHelper.ShowLoaderAsync();
                SearchText = string.IsNullOrWhiteSpace(SearchText) ? "" : SearchText;
                Channels = await _channelService.SearchChannelAsync(0, SearchText);
                if (Channels == null || Channels.Count <= 0)
                {
                    IsShowDefaultText = true;
                    NoRecordFound = "No record found.";
                }
                await _commonHelper.HideLoaderAsync();
            }
            catch (Exception ex)
            {
                await _commonHelper.HideLoaderAsync();
            }
        }

        private void HasNetworkAvailable()
        {
            if (!_commonHelper.HasNetwork())
            {
                IsShowDefaultText = true;
                NoRecordFound = "No network found.";
                Channels = null;
                return;
            }
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
            await _navigationService.NavigateAsync("ChannelSubscribePage", param, false, false);
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
