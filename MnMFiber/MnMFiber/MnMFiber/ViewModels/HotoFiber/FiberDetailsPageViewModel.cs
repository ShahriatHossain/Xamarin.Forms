using MnMFiber.Common.Helper;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models;
using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class FiberDetailsPageViewModel : BindableBase, IBaseInterface
    {
        INavigationService _navigationService;
        IHotoFiberService _hotoFiberRepository;
        public FiberDetailsPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
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

        public async void InitiatePageAsync()
        {
            TicketSpotCable = HotoFiberSession.Current.TicketSpot.TicketSpotCableModel;

            var inputHelp = await _hotoFiberRepository.GetInputHelp();
            if (inputHelp != null)
            {
                GenerateFiberConstructionTypeListAsync(inputHelp);
            }
        }

        private ObservableCollection<CustomCategory> _fiberConstructionTypeList;
        public ObservableCollection<CustomCategory> FiberConstructionTypeList
        {
            get { return _fiberConstructionTypeList; }
            set { SetProperty(ref _fiberConstructionTypeList, value); }
        }

        private CustomCategory _fiberConstructionType;
        public CustomCategory FiberConstructionType
        {
            get { return _fiberConstructionType; }
            set { SetProperty(ref _fiberConstructionType, value); }
        }

        private void GenerateFiberConstructionTypeListAsync(dynamic inputHelp)
        {
            FiberConstructionTypeList = new ObservableCollection<CustomCategory>();

            foreach (var item in inputHelp.ConstructionTypes)
            {
                FiberConstructionTypeList.Add(new CustomCategory { Id = item.Id, Name = item.Name });
            }

            FiberConstructionType = FiberConstructionTypeList.FirstOrDefault(x => x.Id == TicketSpotCable.FiberConstructionTypeId);
        }

        private ObservableCollection<CustomCategory> _fiberTypeList;
        public ObservableCollection<CustomCategory> FiberTypeList
        {
            get { return _fiberTypeList; }
            set { SetProperty(ref _fiberTypeList, value); }
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveToSession));

        public void SaveToSession()
        {
            if (FiberConstructionType == null)
            {
                CommonHelper.DisplayAlertMessage("", "Fiber construction is required");
                return;
            }

            TicketSpotCable.FiberConstructionTypeId = FiberConstructionType.Id;

            HotoFiberSession.Current.TicketSpot.TicketSpotCableModel = TicketSpotCable;

            HotoFiberSession.Current.SpotCategoryFlags.IsFiberDetailsVisited = true;

            _navigationService.NavigateAsync("AddSpotPage", null, false, false);
        }
    }
}
