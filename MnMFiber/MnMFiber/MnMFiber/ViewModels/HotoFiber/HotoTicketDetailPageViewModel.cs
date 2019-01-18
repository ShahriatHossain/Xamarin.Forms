using MnMFiber.Common.Helper;
using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class HotoTicketDetailPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IHotoFiberService _hotoFiberRepository;

        public HotoTicketDetailPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
        {
            _navigationService = navigationService;
            _hotoFiberRepository = hotoFiberRepository;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            TicketDetail = HotoFiberSession.Current.TicketBasic;
        }

        private TicketBasicDto _ticketDetail;
        public TicketBasicDto TicketDetail
        {
            get { return _ticketDetail; }
            set { SetProperty(ref _ticketDetail, value); }
        }

        private DelegateCommand _gotoAddSpotCommand;
        public DelegateCommand GotoAddSpotCommand => _gotoAddSpotCommand ??
            (_gotoAddSpotCommand = new DelegateCommand(GotoAddSpotPageAsync));

        private async void GotoAddSpotPageAsync()
        {
            HotoFiberSession.Current.SetAddSpotBackPage("HotoTicketDetailPage");
            await _navigationService.NavigateAsync("AddSpotPage", null, false, false);
        }

        private DelegateCommand _gotoViewSpotsCommand;
        public DelegateCommand GotoViewSpotsCommand => _gotoViewSpotsCommand ??
            (_gotoViewSpotsCommand = new DelegateCommand(GotoViewSpotsPageAsync));

        private async void GotoViewSpotsPageAsync()
        {
            await _navigationService.NavigateAsync("ViewSpotsPage", null, false, false);
        }

        private DelegateCommand _hotoCompleteCommand;
        public DelegateCommand HotoCompleteCommand => _hotoCompleteCommand != null ?
            _hotoCompleteCommand : (_hotoCompleteCommand = new DelegateCommand(CompleteHotoAsync));

        private async void CompleteHotoAsync()
        {
            var isCompleteHoto = await CommonHelper.AlertMessageWithCancelOrOkAsync("", "Are you sure, you want to complete HOTO?");
            if (isCompleteHoto)
                await _navigationService.NavigateAsync("SignaturesPage", null, false, false);
        }


        private DelegateCommand _navToBackCommand;
        public DelegateCommand NavToBackCommand => _navToBackCommand != null ?
            _navToBackCommand : (_navToBackCommand = new DelegateCommand(NavToBackAsync));

        private async void NavToBackAsync()
        {
            await _navigationService.NavigateAsync("HotoTicketListPage", null, false, false);
        }
    }
}
