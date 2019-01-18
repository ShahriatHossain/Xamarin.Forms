using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Pigeon.ViewModels
{
    public class ChannelSettingPageViewModel : BindableBase
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        public DelegateCommand NavigateToChangeMobileNoPageCommand { get; set; }
        public DelegateCommand NavigateToNotificationPageCommand { get; set; }
        public ChannelApi channel { get; set; }

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

        public ChannelSettingPageViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            NavigateToChangeMobileNoPageCommand = new DelegateCommand(NavigateToMobileNoPage);
            NavigateToNotificationPageCommand = new DelegateCommand(NavigateToNotificationPage);
        }

        private async void NavigateToMobileNoPage()
        {

            await _navigationService.NavigateAsync("MobileNoChangePage", null, false, false);
        }

        private async void NavigateToNotificationPage()
        {

            await _navigationService.NavigateAsync("FaqPage", null, false, false);
        }


    }
}
