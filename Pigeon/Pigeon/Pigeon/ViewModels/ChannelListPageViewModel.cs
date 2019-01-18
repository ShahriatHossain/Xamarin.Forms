using Pigeon.Helpers;
using Pigeon.Services.Configuration;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace Pigeon.ViewModels
{
    public class ChannelListPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IChannelService _channelService;
        private CommonHelper _commonHelper;

        public ChannelListPageViewModel(INavigationService navigationService, IChannelService channelService)
        {
            _navigationService = navigationService;
            _channelService = channelService;
            _commonHelper = new CommonHelper();

            this.SetDefaultFieldsAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.SetDefaultFieldsAsync();
        }

        private ObservableCollection<ChannelApi> _myChannelList;
        public ObservableCollection<ChannelApi> MyChannelList
        {
            get { return _myChannelList; }
            set { SetProperty(ref _myChannelList, value); }
        }

        private ObservableCollection<ChannelApi> _subscribedChannelList;
        public ObservableCollection<ChannelApi> SubscribedChannelList
        {
            get { return _subscribedChannelList; }
            set { SetProperty(ref _subscribedChannelList, value); }
        }

        private bool _hasRecordForSubscribeChannel;
        public bool HasRecordForSubscribeChannel
        {
            get { return _hasRecordForSubscribeChannel; }
            set { SetProperty(ref _hasRecordForSubscribeChannel, value); }
        }

        private bool _hasRecordForMyChannel;
        public bool HasRecordForMyChannel
        {
            get { return _hasRecordForMyChannel; }
            set { SetProperty(ref _hasRecordForMyChannel, value); }
        }

        public async void SetDefaultFieldsAsync()
        {
            try
            {
                MyChannelList = await _channelService.GetChannelByUserAsync(1, string.Empty);

                SubscribedChannelList = await _channelService.GetSubscribeChannelByUserAsync(UserSession.Current.LastRefreshDatetime);

                UserSession.Current.SetLastRefreshDatetime(System.DateTime.Now.ToString("dd/M/yyyy HH:mm:ss"));

                if (SubscribedChannelList != null)
                {
                    foreach (var item in SubscribedChannelList)
                    {
                        _commonHelper.SubscribeChannelForNotification(item.Id.ToString());
                    }
                }

                HasRecordForMyChannel = (MyChannelList.Count >= 1) ? false : true;
                HasRecordForSubscribeChannel = (SubscribedChannelList.Count >= 1) ? false : true;
            }
            catch (Exception ex)
            {

            }

        }

        public async void ChannelDetail(ChannelApi channel)
        {
            try
            {
                var param = new NavigationParameters();
                param.Add("channelId", channel.Id);
                param.Add("cStatus", string.Empty);

                InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(channel.Id));

                await _navigationService.NavigateAsync("ChannelDetailViewer", param, false, false);
            }
            catch (Exception ex)
            {

            }
        }

        DelegateCommand _LoadLargeImage;
        public DelegateCommand LoadLargeImage => _LoadLargeImage != null ?
            _LoadLargeImage : (_LoadLargeImage = new DelegateCommand(LoadLargeImageAsync));

        private void LoadLargeImageAsync()
        {

        }

        DelegateCommand<string> _EditChannelInfo;
        public DelegateCommand<string> EditChannelInfo => _EditChannelInfo != null ?
            _EditChannelInfo : (_EditChannelInfo = new DelegateCommand<string>(EditMyChannelInfo));

        private async void EditMyChannelInfo(string id)
        {
            int channelId = Convert.ToInt32(id);

            var param = new NavigationParameters();
            param.Add("channelId", channelId);
            param.Add("cStatus", string.Empty);
            await _navigationService.NavigateAsync("ChannelEditPage", param, false, false);
        }
    }
}
