using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class TypeOfCrossingsPageViewModel : BindableBase, IBaseInterface
    {
        INavigationService _navigationService;
        IHotoFiberService _hotoFiberRepository;

        public TypeOfCrossingsPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
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

        private TicketSpotCrossingModel _ticketSpotCrossingCable;
        public TicketSpotCrossingModel TicketSpotCrossingCable
        {
            get { return _ticketSpotCrossingCable; }
            set { SetProperty(ref _ticketSpotCrossingCable, value); }
        }

        private TypeOfCrossingFlagsDto _typeOfCrossingFlags;
        public TypeOfCrossingFlagsDto TypeOfCrossingFlags
        {
            get { return _typeOfCrossingFlags; }
            set { SetProperty(ref _typeOfCrossingFlags, value); }
        }

        public void InitiatePageAsync()
        {
            TicketSpotCrossingCable = HotoFiberSession.Current.TicketSpot.TicketSpotCrossingModel;

            TypeOfCrossingFlags = HotoFiberSession.Current.TypeOfCrossingFlags;

            InitiateCheckBoxValues();
        }

        private void InitiateCheckBoxValues()
        {
            TypeOfCrossingFlags.IsBridgeCrossing = Convert.ToBoolean(TicketSpotCrossingCable.BridgeCrossing);
            TypeOfCrossingFlags.IsRailwayCrossing = Convert.ToBoolean(TicketSpotCrossingCable.RailwayCrossing);
            TypeOfCrossingFlags.IsCulvertCrossing = Convert.ToBoolean(TicketSpotCrossingCable.CulvertCrossing);
            TypeOfCrossingFlags.IsRoadCrossing = Convert.ToBoolean(TicketSpotCrossingCable.RoadCrossing);
            TypeOfCrossingFlags.IsOthersCrossing = Convert.ToBoolean(TicketSpotCrossingCable.OtherCrossing);
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveToSession));

        public void SaveToSession()
        {
            HotoFiberSession.Current.TicketSpot.TicketSpotCrossingModel = TicketSpotCrossingCable;

            HotoFiberSession.Current.SpotCategoryFlags.IsTypeOfCrossingsVisited = true;

            _navigationService.NavigateAsync("AddSpotPage", null, false, false);
        }

        internal void EnableCrossingFlagsBlock(string crossingBlock, bool value)
        {
            switch (crossingBlock)
            {
                case "bridge":
                    TypeOfCrossingFlags.IsBridgeCrossing = value;
                    HotoFiberSession.Current.TypeOfCrossingFlags.IsBridgeCrossing = value;
                    break;
                case "railway":
                    TypeOfCrossingFlags.IsRailwayCrossing = value;
                    HotoFiberSession.Current.TypeOfCrossingFlags.IsRailwayCrossing = value;
                    break;
                case "culvert":
                    TypeOfCrossingFlags.IsCulvertCrossing = value;
                    HotoFiberSession.Current.TypeOfCrossingFlags.IsCulvertCrossing = value;
                    break;
                case "road":
                    TypeOfCrossingFlags.IsRoadCrossing = value;
                    HotoFiberSession.Current.TypeOfCrossingFlags.IsRoadCrossing = value;
                    break;
                case "others":
                    TypeOfCrossingFlags.IsOthersCrossing = value;
                    HotoFiberSession.Current.TypeOfCrossingFlags.IsOthersCrossing = value;
                    break;
            }

            HotoFiberSession.Current.SetTypeOfCrossingFlags(HotoFiberSession.Current.TypeOfCrossingFlags);
        }
    }
}
