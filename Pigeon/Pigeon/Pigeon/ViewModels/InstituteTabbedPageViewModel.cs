using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Pigeon.ViewModels
{
    public class InstituteTabbedPageViewModel : BindableBase
    {
        INavigationService _navigationService;
        public InstituteTabbedPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        DelegateCommand _channelSearchCommand;
        public DelegateCommand ChannelSearchCommand => _channelSearchCommand != null ?
            _channelSearchCommand : (_channelSearchCommand = new DelegateCommand(NavigateToChannelSearch));

        private async void NavigateToChannelSearch()
        {
            await _navigationService.NavigateAsync("InstituteDiscoverPage", null, false, false);
        }

        DelegateCommand _channelSettingCommand;
        public DelegateCommand ChannelSettingCommand => _channelSettingCommand != null ?
            _channelSettingCommand : (_channelSettingCommand = new DelegateCommand(NavigateToChannelSetting));

        private async void NavigateToChannelSetting()
        {
            await _navigationService.NavigateAsync("ChannelSettingPage", null, false, false);
        }

        DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand => _refreshCommand != null ?
            _refreshCommand : (_refreshCommand = new DelegateCommand(Refresh));

        private async void Refresh()
        {
            await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);
        }
    }
}
