using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class ViewSpotsPageViewModel : BindableBase, IBaseInterface
    {
        private readonly INavigationService _navigationService;
        private readonly IHotoFiberService _hotoFiberRepository;

        public ViewSpotsPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
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

        private ObservableCollection<TicketSpotDto> _spotList;
        public ObservableCollection<TicketSpotDto> SpotList
        {
            get { return _spotList; }
            set { SetProperty(ref _spotList, value); }
        }

        public async void InitiatePageAsync()
        {
            var spotList = await _hotoFiberRepository.GetTicketSpots(HotoFiberSession.Current.TicketBasic.TicketId);
            SpotList = new ObservableCollection<TicketSpotDto>(spotList.OrderBy(s => s.SerialNo));
        }

        public void SaveToSession()
        {

        }

        internal async void GoToSpotDetailPage(TicketSpotDto selectedItem)
        {
            var ticketSpot = await _hotoFiberRepository.GetTicketSpotDetails(selectedItem.Id);

            HotoFiberSession.Current.SetTicketSpot(ticketSpot);

            HotoFiberSession.Current.SetSpotCategoryFlags(new SpotCategoryFlagsDto { });

            HotoFiberSession.Current.SetAddSpotBackPage("ViewSpotsPage");
            await _navigationService.NavigateAsync("AddSpotPage", null, false, false);
        }

        private DelegateCommand _navToBackCommand;
        public DelegateCommand NavToBackCommand => _navToBackCommand != null ?
            _navToBackCommand : (_navToBackCommand = new DelegateCommand(NavToBackAsync));

        private async void NavToBackAsync()
        {
            await _navigationService.NavigateAsync("HotoTicketDetailPage", null, false, false);
        }
    }
}
