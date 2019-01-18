using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Core.Repositories;
using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class HotoTicketListPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IHotoFiberService _hotoFiberRepository;

        public HotoTicketListPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
        {
            _navigationService = navigationService;
            _hotoFiberRepository = hotoFiberRepository;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            InitiatePage();
        }

        private ObservableCollection<TicketBasicDto> _ticketList;
        public ObservableCollection<TicketBasicDto> TicketList
        {
            get { return _ticketList; }
            set { SetProperty(ref _ticketList, value); }
        }
        private async void InitiatePage()
        {
            TicketList = await _hotoFiberRepository.GetPendingTicketsAsync(UserSession.Current.UserNumber);
        }

        public async void GoToTicketDetailPage(TicketBasicDto ticketDetail)
        {
            InitiateSessions(ticketDetail);

            await _navigationService.NavigateAsync("HotoTicketDetailPage", null, false, false);
        }

        private static void InitiateSessions(TicketBasicDto ticketDetail)
        {
            HotoFiberSession.Current.SetTicketBasic(ticketDetail);
            HotoFiberSession.Current.SetTicketSpot(new TicketSpotDto { });
            HotoFiberSession.Current.TicketSpot.TicketSpotCableModel = new TicketSpotCableModel { };
            HotoFiberSession.Current.TicketSpot.TicketSpotCrossingModel = new TicketSpotCrossingModel { };
            HotoFiberSession.Current.SetSpotCategoryFlags(new SpotCategoryFlagsDto { });
            HotoFiberSession.Current.SetSignatureFlags(new SignatureFlagsDto { });
            HotoFiberSession.Current.SetSignatureDetail(new TicketSignatureUploadModel { });
            HotoFiberSession.Current.SetMediaFiles(new MediaDto { });
            HotoFiberSession.Current.SetMemoryStreams(new MemoryStreamDto { });
            HotoFiberSession.Current.SetTypeOfCrossingFlags(new TypeOfCrossingFlagsDto { });
            HotoFiberSession.Current.SetMediaFileFlags(new MediaFileFlagsDto { });
        }

        private DelegateCommand _navToHomeCommand;
        public DelegateCommand NavToHomeCommand => _navToHomeCommand != null ? _navToHomeCommand :
            (_navToHomeCommand = new DelegateCommand(NavToHomeAsync));

        private async void NavToHomeAsync()
        {
            await _navigationService.NavigateAsync("ModuleListPage", null, false, false);
        }
    }
}
