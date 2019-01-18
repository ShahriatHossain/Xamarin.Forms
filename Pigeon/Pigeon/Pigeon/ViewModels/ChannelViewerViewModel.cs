using Pigeon.Events;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pigeon.ViewModels
{
    public class ChannelViewerViewModel : BindableBase
    {
        INavigationService _navigationService;
        IEventAggregator _eventAggregator;
        public DelegateCommand MyChannelCommand { get; set; }
        private readonly IChannelService _channelService;
        private ObservableCollection<ChannelApi> _channels;
        public ObservableCollection<ChannelApi> Channels
        {
            get { return _channels; }
            set { SetProperty(ref _channels, value); }
        }

        private ChannelApi _channelSelected;
        public ChannelApi ChannelSelected
        {
            get { return _channelSelected; }
            set
            {
                if (value != null)
                {
                    _eventAggregator.GetEvent<ChannelSelectedEvent>().Publish(value);
                }
                SetProperty(ref _channelSelected, value);
            }
        }

        public ChannelViewerViewModel(IChannelService channelService, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _channelService = channelService;
            MyChannelCommand = new DelegateCommand(GoToMyChannel);
            this.ChannelSelected = this.Channels.First();
        }

        private async void GoToMyChannel()
        {
            await _navigationService.NavigateAsync("MyChannelPage", null, false, false);
        }
    }
}
