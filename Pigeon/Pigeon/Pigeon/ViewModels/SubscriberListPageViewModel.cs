using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace Pigeon.ViewModels
{
    public class SubscriberListPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IChannelService _channelService;

        public SubscriberListPageViewModel(INavigationService navigationService, IChannelService channelService)
        {
            _navigationService = navigationService;
            _channelService = channelService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        private int _channelId;
        public int ChannelId
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
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            ChannelId = Convert.ToInt32(parameters["channelId"]);
            CStatus = (string)parameters["cStatus"];
            InitiatePage();
        }

        private ObservableCollection<ChannelSubscribe> _subscriberList;
        public ObservableCollection<ChannelSubscribe> SubscriberList
        {
            get { return _subscriberList; }
            set { SetProperty(ref _subscriberList, value); }
        }

        private async void InitiatePage()
        {
            try
            {
                SubscriberList = await _channelService.GetSubscribers(ChannelId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
