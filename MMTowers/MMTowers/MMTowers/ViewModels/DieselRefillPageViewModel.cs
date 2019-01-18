using MMTowers.Helper;
using MMTowers.Service.Common;
using MMTowers.Service.Interface;
using MMTowers.Service.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MMTowers.ViewModels
{
    public class DieselRefillPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly ITowerService _towerService;
        private readonly IDieselRefillService _dieselRefillService;
        public DieselRefillPageViewModel(INavigationService navigationService, ITowerService towerService, IDieselRefillService dieselRefillService)
        {
            _navigationService = navigationService;
            _towerService = towerService;
            _dieselRefillService = dieselRefillService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.Count > 0)
            {
                GetTowerDetail(parameters);
            }

        }

        private int _btsId;
        public int BtsId
        {
            get { return _btsId; }
            set { SetProperty(ref _btsId, value); }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value); }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value); }
        }

        private string _btsCode;
        public string BtsCode
        {
            get { return _btsCode; }
            set { SetProperty(ref _btsCode, value); }
        }

        private string _btsName;
        public string BtsName
        {
            get { return _btsName; }
            set { SetProperty(ref _btsName, value); }
        }

        private string _btsAddress;
        public string BtsAddress
        {
            get { return _btsAddress; }
            set { SetProperty(ref _btsAddress, value); }
        }

        private async void GetTowerDetail(NavigationParameters parameters)
        {
            BtsId = (int)parameters["btsId"];
            var btsDetail = await _towerService.GetDetailAsync(BtsId);
            if (btsDetail != null)
            {
                BtsCode = btsDetail.BTSCode;
                BtsName = btsDetail.BTSName;
                Latitude = (btsDetail.Latitude == null) ? 0.0 : (Double)btsDetail.Latitude;
                Longitude = (btsDetail.Longitude == null) ? 0.0 : (Double)btsDetail.Longitude;
                BtsAddress = btsDetail.BTSAddress;
            }
        }

        private string _initialQuantity;
        public string InitialQuantity
        {
            get { return _initialQuantity; }
            set { SetProperty(ref _initialQuantity, value); }
        }

        private string _filledQuantity;
        public string FilledQuantity
        {
            get { return _filledQuantity; }
            set { SetProperty(ref _filledQuantity, value); }
        }

        private MediaFile _hmrPhoto;
        public MediaFile HMRPhoto
        {
            get { return _hmrPhoto; }
            set { SetProperty(ref _hmrPhoto, value); }
        }

        private MediaFile _ebPhoto;
        public MediaFile EBPhoto
        {
            get { return _ebPhoto; }
            set { SetProperty(ref _ebPhoto, value); }
        }

        private MediaFile _gsuPhoto;
        public MediaFile GSUPhoto
        {
            get { return _gsuPhoto; }
            set { SetProperty(ref _gsuPhoto, value); }
        }

        private MediaFile _dieselBillPhoto;
        public MediaFile DieselBillPhoto
        {
            get { return _dieselBillPhoto; }
            set { SetProperty(ref _dieselBillPhoto, value); }
        }

        DelegateCommand _takeHMRPhotoCommand;
        public DelegateCommand TakeHMRPhotoCommand => _takeHMRPhotoCommand != null ?
            _takeHMRPhotoCommand : (_takeHMRPhotoCommand = new DelegateCommand(TakeHMRPhoto));
        private async void TakeHMRPhoto()
        {
            await TakePhotoFromCamera("HMRPhoto");
        }

        DelegateCommand _takeEBPhotoCommand;
        public DelegateCommand TakeEBPhotoCommand => _takeEBPhotoCommand != null ?
            _takeEBPhotoCommand : (_takeEBPhotoCommand = new DelegateCommand(TakeEBPhoto));
        private async void TakeEBPhoto()
        {
            await TakePhotoFromCamera("EBPhoto");
        }

        private bool _hmrPhotoUploaded = false;
        public bool HMRPhotoUploaded
        {
            get { return _hmrPhotoUploaded; }
            set { SetProperty(ref _hmrPhotoUploaded, value); }
        }

        private bool _ebPhotoUploaded = false;
        public bool EBPhotoUploaded
        {
            get { return _ebPhotoUploaded; }
            set { SetProperty(ref _ebPhotoUploaded, value); }
        }

        private bool _gsuPhotoUploaded = false;
        public bool GSUPhotoUploaded
        {
            get { return _gsuPhotoUploaded; }
            set { SetProperty(ref _gsuPhotoUploaded, value); }
        }

        private bool _dieselBillPhotoUploaded = false;
        public bool DieselBillPhotoUploaded
        {
            get { return _dieselBillPhotoUploaded; }
            set { SetProperty(ref _dieselBillPhotoUploaded, value); }
        }

        private async Task TakePhotoFromCamera(string photoType)
        {

            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg",
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 92
                });

                if (file == null)
                    return;

                switch (photoType)
                {
                    case "HMRPhoto":
                        HMRPhoto = file;
                        HMRPhotoUploaded = true;
                        break;
                    case "EBPhoto":
                        EBPhoto = file;
                        EBPhotoUploaded = true;
                        break;
                    case "GSUPhoto":
                        GSUPhoto = file;
                        GSUPhotoUploaded = true;
                        break;
                    case "DieselBillPhoto":
                        DieselBillPhoto = file;
                        DieselBillPhotoUploaded = true;
                        break;
                }

                CommonHelper.DisplayAlertMessage("", "Photo uploaded successfully.");
            }
            catch (Exception ex)
            {
                CommonHelper.DisplayAlertMessage("", "Failed to upload photo.");
            }

        }

        DelegateCommand _takeGSUPhotoCommand;
        public DelegateCommand TakeGSUPhotoCommand => _takeGSUPhotoCommand != null ?
            _takeGSUPhotoCommand : (_takeGSUPhotoCommand = new DelegateCommand(TakeGSUPhoto));
        private async void TakeGSUPhoto()
        {
            await TakePhotoFromCamera("GSUPhoto");
        }

        DelegateCommand _takeDieselBillPhotoCommand;
        public DelegateCommand TakeDieselBillPhotoCommand => _takeDieselBillPhotoCommand != null ?
            _takeDieselBillPhotoCommand : (_takeDieselBillPhotoCommand = new DelegateCommand(TakeDieselBillPhoto));
        private async void TakeDieselBillPhoto()
        {
            await TakePhotoFromCamera("DieselBillPhoto");
        }

        DelegateCommand _refillDieselCommand;
        public DelegateCommand RefillDieselCommand => _refillDieselCommand != null ?
            _refillDieselCommand : (_refillDieselCommand = new DelegateCommand(SaveDieselRefillAsync));

        private async void SaveDieselRefillAsync()
        {

            try
            {
                await CommonHelper.ShowLoaderAsync();

                if (!CommonHelper.HasInternetConnection())
                {
                    await CommonHelper.HideLoaderAsync();

                    CommonHelper.DisplayAlertMessage("", "Please check your internet connection.");
                    return;
                }
                else if (string.IsNullOrEmpty(InitialQuantity) || string.IsNullOrEmpty(FilledQuantity))
                {
                    await CommonHelper.HideLoaderAsync();

                    CommonHelper.DisplayAlertMessage("", "Initial and Filled Quantity value required");
                }
                else
                {
                    var hmrPhoto = (HMRPhoto == null) ? string.Empty : Path.GetFileName(HMRPhoto.Path);
                    var ebPhoto = (EBPhoto == null) ? string.Empty : Path.GetFileName(EBPhoto.Path);
                    var gsuPhoto = (GSUPhoto == null) ? string.Empty : Path.GetFileName(GSUPhoto.Path);
                    var dieselBillPhoto = (DieselBillPhoto == null) ? string.Empty : Path.GetFileName(DieselBillPhoto.Path);

                    await _dieselRefillService.UploadRefillImageAsync(HMRPhoto, EBPhoto, GSUPhoto, DieselBillPhoto);

                    var dieselRefill = new DieselRefill()
                    {
                        BTSId = BtsId,
                        RefillBy = UserSession.Current.UserName,
                        InitialQuantity = Convert.ToInt32(InitialQuantity),
                        FilledQuantity = Convert.ToInt32(FilledQuantity),
                        HMRPhoto = hmrPhoto,
                        EBPhoto = ebPhoto,
                        GSUPhoto = gsuPhoto,
                        DieselBillPhoto = dieselBillPhoto
                    };
                    var response = await _dieselRefillService.RefillDieselAsync(dieselRefill);

                    if (!string.IsNullOrEmpty(response))
                    {
                        await _navigationService.NavigateAsync("TowerListPage", null, false, false);

                        CommonHelper.DisplayAlertMessage("", "Refill details updated successfully.");
                    }

                    await CommonHelper.HideLoaderAsync();
                }
            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();

                CommonHelper.DisplayAlertMessage("", "Refill details failed to update.");
            }


        }

        DelegateCommand _resetCommand;
        public DelegateCommand ResetCommand => _resetCommand != null ?
            _resetCommand : (_resetCommand = new DelegateCommand(ResetAsync));

        private async void ResetAsync()
        {
            await _navigationService.NavigateAsync("TowerListPage", null, false, false);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}
