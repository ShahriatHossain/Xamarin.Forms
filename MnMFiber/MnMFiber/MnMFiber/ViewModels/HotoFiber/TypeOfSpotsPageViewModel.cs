using MnMFiber.Common.Helper;
using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class TypeOfSpotsPageViewModel : BindableBase, IBaseInterface
    {
        private readonly INavigationService _navigationService;
        private readonly IHotoFiberService _hotoFiberRepository;

        public TypeOfSpotsPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
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

        private TypeOfSpotFlagsDto _typeOfSpotsFlag;
        public TypeOfSpotFlagsDto TypeOfSpotsFlag
        {
            get { return _typeOfSpotsFlag; }
            set { SetProperty(ref _typeOfSpotsFlag, value); }
        }

        private TicketSpotDto _ticketSpot;
        public TicketSpotDto TicketSpot
        {
            get { return _ticketSpot; }
            set { SetProperty(ref _ticketSpot, value); }
        }
        public async void InitiatePageAsync()
        {
            TypeOfSpotsFlag = new TypeOfSpotFlagsDto { IsTypeTower = true };

            InitiateTicketSpot();

            var inputHelp = await _hotoFiberRepository.GetInputHelp();
            if (inputHelp != null)
            {
                GenerateTypeOfSpot(inputHelp);

                GenerateFiberPlacedList(inputHelp);
            }
        }


        private CustomCategory _fiberPlaced;
        public CustomCategory FiberPlaced
        {
            get { return _fiberPlaced; }
            set { SetProperty(ref _fiberPlaced, value); }
        }

        private ObservableCollection<CustomCategory> _fiberPlacedList;
        public ObservableCollection<CustomCategory> FiberPlacedList
        {
            get { return _fiberPlacedList; }
            set { SetProperty(ref _fiberPlacedList, value); }
        }
        private void GenerateFiberPlacedList(dynamic inputHelp)
        {
            FiberPlacedList = new ObservableCollection<CustomCategory>();

            foreach (var item in inputHelp.FiberPlaced)
            {
                FiberPlacedList.Add(new CustomCategory { Id = item.Id, Name = item.Name });
            }

            FiberPlaced = (TicketSpot.FiberPlacedId > 0) ? FiberPlacedList.FirstOrDefault(f => f.Id == TicketSpot.FiberPlacedId) : FiberPlacedList[0];
        }

        private void InitiateTicketSpot()
        {
            TicketSpot = HotoFiberSession.Current.TicketSpot;
        }

        private ObservableCollection<CustomCategory> _typeOfSpotList;
        public ObservableCollection<CustomCategory> TypeOfSpotList
        {
            get { return _typeOfSpotList; }
            set { SetProperty(ref _typeOfSpotList, value); }
        }

        private CustomCategory _typeOfSpot;
        public CustomCategory TypeOfSpot
        {
            get { return _typeOfSpot; }
            set { SetProperty(ref _typeOfSpot, value); }
        }
        private void GenerateTypeOfSpot(dynamic inputHelp)
        {
            try
            {
                TypeOfSpotList = new ObservableCollection<CustomCategory>();

                foreach (var item in inputHelp.SpotTypes)
                {
                    TypeOfSpotList.Add(new CustomCategory { Id = item.Id, Name = item.Name });
                }

                TypeOfSpot = (TicketSpot.SpotTypeId > 0) ?
                    TypeOfSpotList.FirstOrDefault(x => x.Id == TicketSpot.SpotTypeId) : TypeOfSpotList[0];
            }
            catch (Exception ex)
            {

            }

        }

        public void ShowFieldsRelatedToSpotType(CustomCategory typeOfSpot)
        {
            switch (typeOfSpot.Name.ToLower())
            {
                case "tower":
                    InitiateTypeOfSpotFlags();
                    TypeOfSpotsFlag.IsTypeTower = true;
                    TicketSpot.SpotTypeId = typeOfSpot.Id;
                    break;

                case "manhole":
                case "handhole":
                    InitiateTypeOfSpotFlags();
                    TypeOfSpotsFlag.IsTypeManhole = true;
                    TicketSpot.SpotTypeId = typeOfSpot.Id;
                    break;

                case "route marker":
                    InitiateTypeOfSpotFlags();
                    TypeOfSpotsFlag.IsTypeRouteMarker = true;
                    TicketSpot.SpotTypeId = typeOfSpot.Id;
                    break;

                case "fiber on route":
                    InitiateTypeOfSpotFlags();
                    TypeOfSpotsFlag.IsFiberOnRoute = true;
                    TicketSpot.SpotTypeId = typeOfSpot.Id;
                    break;

                case "others":
                    InitiateTypeOfSpotFlags();
                    TypeOfSpotsFlag.IsTypeOthers = true;
                    TicketSpot.SpotTypeId = typeOfSpot.Id;
                    break;
            }
        }

        private void InitiateTypeOfSpotFlags()
        {
            TypeOfSpotsFlag.IsTypeTower = false;
            TypeOfSpotsFlag.IsTypeManhole = false;
            TypeOfSpotsFlag.IsTypeHandHole = false;
            TypeOfSpotsFlag.IsTypeRouteMarker = false;
            TypeOfSpotsFlag.IsFiberOnRoute = false;
            TypeOfSpotsFlag.IsTypeOthers = false;
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveToSession));

        public void SaveToSession()
        {
            if (TicketSpot.TwoPointsDistance is null || TicketSpot.TwoPointsDistance < 0)
            {
                CommonHelper.DisplayAlertMessage("", "Distance Between Two Points field is required.");
                return;
            }

            TicketSpot.SpotTypeId = TypeOfSpot.Id;
            TicketSpot.FiberPlacedId = FiberPlaced.Id;
            HotoFiberSession.Current.SetTicketSpot(TicketSpot);

            HotoFiberSession.Current.SpotCategoryFlags.IsTypeOfSpotsVisited = true;

            _navigationService.NavigateAsync("AddSpotPage", null, false, false);
        }
    }
}
