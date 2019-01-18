using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class CableDetailPageViewModel : BindableBase, IBaseInterface
    {
        INavigationService _navigationService;
        IHotoFiberService _hotoFiberRepository;

        public CableDetailPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
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

        private TicketSpotCableModel _ticketSpotCable;
        public TicketSpotCableModel TicketSpotCable
        {
            get { return _ticketSpotCable; }
            set { SetProperty(ref _ticketSpotCable, value); }
        }
        public void InitiatePageAsync()
        {
            TicketSpotCable = HotoFiberSession.Current.TicketSpot.TicketSpotCableModel;
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveToSession));

        public void SaveToSession()
        {
            HotoFiberSession.Current.TicketSpot.TicketSpotCableModel = new TicketSpotCableModel();
            HotoFiberSession.Current.TicketSpot.TicketSpotCableModel = TicketSpotCable;

            HotoFiberSession.Current.SpotCategoryFlags.IsCableDetailVisited = true;

            _navigationService.NavigateAsync("AddSpotPage", null, false, false);
        }
    }
}
