using MnMFiber.Common.Helper;
using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models;
using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class AddSpotPageViewModel : BindableBase, IBaseInterface
    {
        private readonly INavigationService _navigationService;
        private readonly IHotoFiberService _hotoFiberRepository;

        public AddSpotPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
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


        private TicketBasicDto _ticketDetail;
        public TicketBasicDto TicketDetail
        {
            get { return _ticketDetail; }
            set { SetProperty(ref _ticketDetail, value); }
        }

        private ObservableCollection<CustomCategory> _categoryList;
        public ObservableCollection<CustomCategory> CategoryList
        {
            get { return _categoryList; }
            set { SetProperty(ref _categoryList, value); }
        }

        public void InitiatePageAsync()
        {
            TicketDetail = HotoFiberSession.Current.TicketBasic;

            CheckAllTabsFilledUp();

            GenerateCategoryList();
        }

        private bool _isAllTabsFilledUp;
        public bool IsAllTabsFilledUp
        {
            get { return _isAllTabsFilledUp; }
            set { SetProperty(ref _isAllTabsFilledUp, value); }
        }

        private void CheckAllTabsFilledUp()
        {
            IsAllTabsFilledUp = false;

            if (HotoFiberSession.Current.SpotCategoryFlags.IsTypeOfSpotsVisited == true
                && HotoFiberSession.Current.SpotCategoryFlags.IsFiberDetailsVisited == true
                && HotoFiberSession.Current.SpotCategoryFlags.IsCableDetailVisited == true
                && HotoFiberSession.Current.SpotCategoryFlags.IsTypeOfCrossingsVisited == true
                && HotoFiberSession.Current.SpotCategoryFlags.IsAerialCableVisited == true
                && HotoFiberSession.Current.SpotCategoryFlags.IsOtherDetailVisited == true
                && HotoFiberSession.Current.SpotCategoryFlags.IsChambersVisited == true)
            {
                IsAllTabsFilledUp = true;
            }

        }

        private void GenerateCategoryList()
        {
            CategoryList = new ObservableCollection<CustomCategory>
            {
                new CustomCategory { Id = 1, Name = "Type Of Spot", Flag = HotoFiberSession.Current.SpotCategoryFlags.IsTypeOfSpotsVisited},
                new CustomCategory { Id = 2, Name = "Fiber Details", Flag = HotoFiberSession.Current.SpotCategoryFlags.IsFiberDetailsVisited},
                new CustomCategory { Id = 3, Name = "Cable Details", Flag = HotoFiberSession.Current.SpotCategoryFlags.IsCableDetailVisited},
                new CustomCategory { Id = 4, Name = "Type Of Crossings", Flag = HotoFiberSession.Current.SpotCategoryFlags.IsTypeOfCrossingsVisited},
                new CustomCategory { Id = 5, Name = "Aerial Cable Types", Flag = HotoFiberSession.Current.SpotCategoryFlags.IsAerialCableVisited},
                new CustomCategory { Id = 6, Name = "Chambers", Flag = HotoFiberSession.Current.SpotCategoryFlags.IsChambersVisited},
                new CustomCategory { Id = 7, Name = "Other Details", Flag = HotoFiberSession.Current.SpotCategoryFlags.IsOtherDetailVisited}
            };
        }

        public void GoToCategoryDetailPage(int id)
        {
            switch (id)
            {
                case 1:
                    _navigationService.NavigateAsync("TypeOfSpotsPage", null, false, false);
                    break;
                case 2:
                    _navigationService.NavigateAsync("FiberDetailsPage", null, false, false);
                    break;
                case 3:
                    _navigationService.NavigateAsync("CableDetailPage", null, false, false);
                    break;
                case 4:
                    _navigationService.NavigateAsync("TypeOfCrossingsPage", null, false, false);
                    break;
                case 5:
                    _navigationService.NavigateAsync("AerialCablePage", null, false, false);
                    break;
                case 6:
                    _navigationService.NavigateAsync("ChambersPage", null, false, false);
                    break;
                case 7:
                    _navigationService.NavigateAsync("OtherDetailPage", null, false, false);
                    break;
            }
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            try
            {
                HotoFiberSession.Current.TicketSpot.TicketId = HotoFiberSession.Current.TicketBasic.TicketId;

                await CommonHelper.ShowLoaderAsync();

                var isLocationSet = await SetLatLong();

                if (isLocationSet)
                {
                    await SetSpotImage();

                    var response = await _hotoFiberRepository.SaveTicketSpot(HotoFiberSession.Current.TicketSpot);
                    if (response != null && response.Success)
                    {
                        RemoveRelatedSessions();

                        await _navigationService.NavigateAsync(HotoFiberSession.Current.AddSpotBackPage, null, false, false);

                        await CommonHelper.HideLoaderAsync();
                        CommonHelper.DisplayAlertMessage("", "Successfully added spot.");
                    }
                    else
                    {
                        await CommonHelper.HideLoaderAsync();
                        CommonHelper.DisplayAlertMessage("", "Save failed. Please try again.");
                    }
                }


            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();
            }

        }

        private async System.Threading.Tasks.Task SetSpotImage()
        {
            var spotImageFileName = string.Empty;
            var spotImageDisplayName = string.Empty;

            if (HotoFiberSession.Current.MediaFiles.Media1 != null)
            {
                spotImageDisplayName = Path.GetFileName(HotoFiberSession.Current.MediaFiles.Media1.Path);
                spotImageFileName = await _hotoFiberRepository.UploadFileAsync(HotoFiberSession.Current.MediaFiles.Media1);
            }

            HotoFiberSession.Current.TicketSpot.SpotImageFileName = (!string.IsNullOrEmpty(spotImageFileName)) ? spotImageFileName : HotoFiberSession.Current.TicketSpot.SpotImageFileName;
            HotoFiberSession.Current.TicketSpot.SpotImageFileDisplayName = (!string.IsNullOrEmpty(spotImageDisplayName)) ? spotImageDisplayName : HotoFiberSession.Current.TicketSpot.SpotImageFileDisplayName;


            var chamberCoverImageFileName = string.Empty;
            var chamberCoverImageFileDisplayName = string.Empty;

            if (HotoFiberSession.Current.MediaFiles.Media2 != null)
            {
                chamberCoverImageFileDisplayName = Path.GetFileName(HotoFiberSession.Current.MediaFiles.Media2.Path);
                chamberCoverImageFileName = await _hotoFiberRepository.UploadFileAsync(HotoFiberSession.Current.MediaFiles.Media2);
            }

            HotoFiberSession.Current.TicketSpot.ChamberCoverImageFileName = (!string.IsNullOrEmpty(chamberCoverImageFileName)) ? chamberCoverImageFileName : HotoFiberSession.Current.TicketSpot.ChamberCoverImageFileName;
            HotoFiberSession.Current.TicketSpot.ChamberCoverImageFileDisplayName = (!string.IsNullOrEmpty(chamberCoverImageFileDisplayName)) ? chamberCoverImageFileDisplayName : HotoFiberSession.Current.TicketSpot.ChamberCoverImageFileDisplayName;
        }

        private async System.Threading.Tasks.Task<bool> SetLatLong()
        {
            var isLocationSet = true;
            var position = await CommonHelper.GetDeviceCurrentLocation();
            if (position == null)
            {
                await CommonHelper.HideLoaderAsync();
                CommonHelper.DisplayAlertMessage("", "Lattitude and Longitude can't be empty, please enable your device location.");
                isLocationSet = false;
            }
            else if (position.Latitude <= 0 || position.Longitude <= 0)
            {
                await CommonHelper.HideLoaderAsync();
                CommonHelper.DisplayAlertMessage("", "Lattitude and Longitude can't be empty, please enable your device location.");
                isLocationSet = false;
            }

            HotoFiberSession.Current.TicketSpot.Latitude = (HotoFiberSession.Current.TicketSpot.Latitude > 0) ? HotoFiberSession.Current.TicketSpot.Latitude : Convert.ToDecimal(position.Latitude);
            HotoFiberSession.Current.TicketSpot.Longitude = (HotoFiberSession.Current.TicketSpot.Longitude > 0) ? HotoFiberSession.Current.TicketSpot.Longitude : Convert.ToDecimal(position.Longitude);

            return isLocationSet;
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand != null ?
            _cancelCommand : (_cancelCommand = new DelegateCommand(CancelAsync));

        private async void CancelAsync()
        {
            RemoveRelatedSessions();

            await _navigationService.NavigateAsync(HotoFiberSession.Current.AddSpotBackPage, null, false, false);
        }

        private void RemoveRelatedSessions()
        {
            HotoFiberSession.Current.SetTicketSpot(new TicketSpotDto { });
            HotoFiberSession.Current.SetSpotCategoryFlags(new SpotCategoryFlagsDto { });
            HotoFiberSession.Current.SetMediaFiles(new MediaDto { });
            HotoFiberSession.Current.SetTypeOfCrossingFlags(new TypeOfCrossingFlagsDto { });
            HotoFiberSession.Current.TicketSpot.TicketSpotCableModel = new TicketSpotCableModel { };
            HotoFiberSession.Current.TicketSpot.TicketSpotCrossingModel = new TicketSpotCrossingModel { };
            HotoFiberSession.Current.SetMediaFileFlags(new MediaFileFlagsDto { });
        }

        public void SaveToSession()
        {

        }
    }
}
