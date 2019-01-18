using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class PhotosAndSignatureContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IFileUploadService _fileUploadService;
        public PhotosAndSignatureContentPageViewModel(INavigationService navigationService, IFileUploadService fileUploadService)
        {
            _navigationService = navigationService;
            _fileUploadService = fileUploadService;
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            BatteryCommissioningSession.Current.SetIsPhotosAndSignatureFilled(true);

            if (IsAllPhotosUploaded())
            {
                _navigationService.GoBackAsync(null, false, false);
            }
        }

        private bool IsAllPhotosUploaded()
        {
            var isReAssignedTicket = BatteryCommissioningSession.Current.ReAssignedToTechnician;
            var isReAssignToAnother = BatteryCommissioningSession.Current.Transaction.IsReAssignToAnother;
            var isTechnicianSubmitted = BatteryCommissioningSession.Current.Transaction.TechnicianSubmitted;

            if (isReAssignedTicket == true && isReAssignToAnother == false && isTechnicianSubmitted == true)
                return true;

            var photo1 = BatteryCommissioningSession.Current.Photo1;
            var photo2 = BatteryCommissioningSession.Current.Photo2;
            var file1 = BatteryCommissioningSession.Current.File1;
            var sign1 = BatteryCommissioningSession.Current.Signature1;
            var sign2 = BatteryCommissioningSession.Current.Signature2;

            if (photo1 == null || photo2 == null || file1 == null || sign1 == null || sign2 == null)
            {
                CommonHelper.DisplayAlertMessage("", "All fields are mandatory.");

                return false;
            }

            return true;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }


        public void OnNavigatedTo(NavigationParameters parameters)
        {
            TransactionId = BatteryCommissioningSession.Current.CommissionTransactionId;
            TransactionNumber = BatteryCommissioningSession.Current.CommissionTransactionNo;

            CheckFormsHasBeenFilled();
        }


        private bool _isSignature1Taken;
        public bool IsSignature1Taken
        {
            get { return _isSignature1Taken; }
            set { SetProperty(ref _isSignature1Taken, value); }
        }

        private bool _isSignature2Taken;
        public bool IsSignature2Taken
        {
            get { return _isSignature2Taken; }
            set { SetProperty(ref _isSignature2Taken, value); }
        }

        private bool _isPhoto1Uploaded;
        public bool IsPhoto1Uploaded
        {
            get { return _isPhoto1Uploaded; }
            set { SetProperty(ref _isPhoto1Uploaded, value); }
        }

        private bool _isPhoto2Uploaded;
        public bool IsPhoto2Uploaded
        {
            get { return _isPhoto2Uploaded; }
            set { SetProperty(ref _isPhoto2Uploaded, value); }
        }

        private bool _isFile1Uploaded;
        public bool IsFile1Uploaded
        {
            get { return _isFile1Uploaded; }
            set { SetProperty(ref _isFile1Uploaded, value); }
        }
        private void CheckFormsHasBeenFilled()
        {
            IsSignature1Taken = BatteryCommissioningSession.Current.IsSignature1TakenFilled;

            IsSignature2Taken = BatteryCommissioningSession.Current.IsSignature2TakenFilled;

            IsPhoto1Uploaded = BatteryCommissioningSession.Current.IsPhoto1UploadedFilled;

            IsPhoto2Uploaded = BatteryCommissioningSession.Current.IsPhoto2UploadedFilled;

            IsFile1Uploaded = BatteryCommissioningSession.Current.IsFile1UploadedFilled;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        DelegateCommand _takeSignature1Command;
        public DelegateCommand TakeSignature1Command => _takeSignature1Command != null ?
            _takeSignature1Command : (_takeSignature1Command = new DelegateCommand(TakeSignature1Async));
        private async void TakeSignature1Async()
        {
            var param = new NavigationParameters();
            param.Add("sign", 1);
            await _navigationService.NavigateAsync("SignatureContentPage", param, false, false);
        }

        DelegateCommand _takeSignature2Command;
        public DelegateCommand TakeSignature2Command => _takeSignature2Command != null ?
            _takeSignature2Command : (_takeSignature2Command = new DelegateCommand(TakeSignature2Async));
        private async void TakeSignature2Async()
        {
            var param = new NavigationParameters();
            param.Add("sign", 2);
            await _navigationService.NavigateAsync("SignatureContentPage", param, false, false);
        }

        DelegateCommand _takePhoto1Command;
        public DelegateCommand TakePhoto1Command => _takePhoto1Command != null ?
            _takePhoto1Command : (_takePhoto1Command = new DelegateCommand(TakePhoto1Async));
        private async void TakePhoto1Async()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92
            });

            if (file == null)
                return;

            BatteryCommissioningSession.Current.SetPhoto1(file);

            BatteryCommissioningSession.Current.SetIsPhoto1UploadedFilled(true);
            IsPhoto1Uploaded = true;
        }

        DelegateCommand _takePhoto2Command;
        public DelegateCommand TakePhoto2Command => _takePhoto2Command != null ?
            _takePhoto2Command : (_takePhoto2Command = new DelegateCommand(TakePhoto2Async));
        private async void TakePhoto2Async()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92
            });

            if (file == null)
                return;

            BatteryCommissioningSession.Current.SetIsPhoto2UploadedFilled(true);
            IsPhoto2Uploaded = true;

            BatteryCommissioningSession.Current.SetPhoto2(file);
        }

        DelegateCommand _takeFile1Command;
        public DelegateCommand TakeFile1Command => _takeFile1Command != null ?
            _takeFile1Command : (_takeFile1Command = new DelegateCommand(TakeFile1Async));
        private async void TakeFile1Async()
        {
            try
            {
                CommonHelper.PromptToStoragePermissionEnabled();

                FileData fileData = new FileData();
                fileData = await CrossFilePicker.Current.PickFile();

                var fileDataArray = new CommonHelper().GetFileDataArray(fileData.FileName);
                if (fileDataArray.Length <= 0)
                {
                    CommonHelper.DisplayAlertMessage(string.Empty, "Document could not be uploaded! Place the Document in local drive and try again.");

                    fileData = null;
                }

                if (fileData != null)
                {
                    BatteryCommissioningSession.Current.SetIsFile1UploadedFilled(true);
                    IsFile1Uploaded = true;
                    BatteryCommissioningSession.Current.SetFile1(fileData);
                }
                else
                {
                    BatteryCommissioningSession.Current.SetIsFile1UploadedFilled(false);
                    IsFile1Uploaded = false;
                    BatteryCommissioningSession.Current.SetFile1(fileData);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
