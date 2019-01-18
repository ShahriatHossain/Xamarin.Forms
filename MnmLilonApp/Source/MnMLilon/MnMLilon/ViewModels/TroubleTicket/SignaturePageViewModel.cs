using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class SignaturePageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        public SignaturePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        public async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsSignatureFilled(true);

            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.CustomerRemarks = CustomerRemarks;

            if (IsAllFilesUploaded())
            {
                await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
            }
        }

        private bool IsAllFilesUploaded()
        {
            var ticketModel = TroubleTicketSession.Current.TroubleTicket;
            var signStr1 = ticketModel.TroubleTicketDetails.AssignedPersonSign;
            var signStr2 = ticketModel.TroubleTicketDetails.CustomerRepresentativeSign;

            var file1 = TroubleTicketSession.Current.File1;
            var sign1 = TroubleTicketSession.Current.Signature1;
            var sign2 = TroubleTicketSession.Current.Signature2;

            if(string.IsNullOrEmpty(signStr1) || string.IsNullOrEmpty(signStr2))
            {
                if (file1 == null || sign1 == null || sign2 == null)
                {

                    CommonHelper.DisplayAlertMessage("", "All fields are mandatory.");

                    return false;
                }
            }

            return true;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
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
        
        private bool _isFile1Uploaded;
        public bool IsFile1Uploaded
        {
            get { return _isFile1Uploaded; }
            set { SetProperty(ref _isFile1Uploaded, value); }
        }

        private string _customerRemarks;
        public string CustomerRemarks
        {
            get { return _customerRemarks; }
            set { SetProperty(ref _customerRemarks, value); }
        }
        private void CheckFormsHasBeenFilled()
        {
            IsSignature1Taken = TroubleTicketSession.Current.IsSignature1TakenFilled;

            IsSignature2Taken = TroubleTicketSession.Current.IsSignature2TakenFilled;

            IsFile1Uploaded = TroubleTicketSession.Current.IsFile1UploadedFilled;

            CustomerRemarks = TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.CustomerRemarks;
        }


        DelegateCommand _takeSignature1Command;
        public DelegateCommand TakeSignature1Command => _takeSignature1Command != null ?
            _takeSignature1Command : (_takeSignature1Command = new DelegateCommand(TakeSignature1Async));
        private async void TakeSignature1Async()
        {
            var param = new NavigationParameters();
            param.Add("sign", 1);
            await _navigationService.NavigateAsync("SignPage", param, false, false);
        }

        DelegateCommand _takeSignature2Command;
        public DelegateCommand TakeSignature2Command => _takeSignature2Command != null ?
            _takeSignature2Command : (_takeSignature2Command = new DelegateCommand(TakeSignature2Async));
        private async void TakeSignature2Async()
        {
            var param = new NavigationParameters();
            param.Add("sign", 2);
            await _navigationService.NavigateAsync("SignPage", param, false, false);
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
                    TroubleTicketSession.Current.SetIsFile1UploadedFilled(true);
                    IsFile1Uploaded = true;
                    TroubleTicketSession.Current.SetFile1(fileData);
                }
                else
                {
                    TroubleTicketSession.Current.SetIsFile1UploadedFilled(false);
                    IsFile1Uploaded = false;
                    TroubleTicketSession.Current.SetFile1(fileData);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
