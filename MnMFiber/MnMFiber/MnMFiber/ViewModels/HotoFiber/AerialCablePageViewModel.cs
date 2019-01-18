using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class AerialCablePageViewModel : BindableBase, IBaseInterface
    {
        INavigationService _navigationService;
        IHotoFiberService _hotoFiberRepository;
        public AerialCablePageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
        {
            _navigationService = navigationService;
            _hotoFiberRepository = hotoFiberRepository;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            InitiatePageAsync();
        }

        private TicketSpotDto _ticketSpot;
        public TicketSpotDto TicketSpot
        {
            get { return _ticketSpot; }
            set { SetProperty(ref _ticketSpot, value); }
        }
        public void InitiatePageAsync()
        {
            TicketSpot = HotoFiberSession.Current.TicketSpot;
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveToSession));

        public void SaveToSession()
        {
            HotoFiberSession.Current.SetTicketSpot(TicketSpot);

            HotoFiberSession.Current.SpotCategoryFlags.IsAerialCableVisited = true;

            _navigationService.NavigateAsync("AddSpotPage", null, false, false);
        }
    }
}
