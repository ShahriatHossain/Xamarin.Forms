using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Pigeon.ViewModels
{
    public class ChannelOptionPageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        public DelegateCommand NavigateToChannelDetailsPageCommand { get; set; }
        public DelegateCommand NavigateToSecureIdPageCommand { get; set; }
        public DelegateCommand GotoParentCommand { get; set; }
        public ChannelApi channel { get; set; }
        private bool _isApproved;
        public bool IsApproved
        {
            get { return _isApproved; }
            set { SetProperty(ref _isApproved, value); }
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

        public ChannelOptionPageViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            NavigateToChannelDetailsPageCommand = new DelegateCommand(NavigateToChannelDetailsPage);
            NavigateToSecureIdPageCommand = new DelegateCommand(NavigateToSecureIdPage);
            GotoParentCommand = new DelegateCommand(GotoParent);
            IsApproved = false;
        }

        private async void GotoParent()
        {
            await _navigationService.NavigateAsync("MyChannelListPage", null, false, false);
        }

        private async void NavigateToChannelDetailsPage()
        {
            var param = new NavigationParameters();
            param.Add("channelId", ChannelId);
            param.Add("cStatus", CStatus);
            await _navigationService.NavigateAsync("ChannelCreatePage", param, false, false);
        }

        private async void NavigateToSecureIdPage()
        {
            var param = new NavigationParameters();
            param.Add("channelId", ChannelId);
            param.Add("cStatus", CStatus);
            await _navigationService.NavigateAsync("ChannelSecureIdPage", param, false, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CStatus = (string)parameters["cStatus"];
            ChannelId = (int?)parameters["channelId"];
            if (CStatus == "A")
            {
                IsApproved = true;
            }
        }
    }
}
