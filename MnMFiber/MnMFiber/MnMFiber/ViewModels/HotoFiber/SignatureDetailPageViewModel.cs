using MnMFiber.Common.Helper;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Persistence.Sessions;
using Prism.Mvvm;
using Prism.Navigation;
using System.IO;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class SignatureDetailPageViewModel : BindableBase, IBaseInterface
    {
        INavigationService _navigationService;

        public SignatureDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            InitiatePageAsync();
        }

        private MemoryStream _signature;
        public MemoryStream Signature
        {
            get { return _signature; }
            set { SetProperty(ref _signature, value); }
        }

        private TicketSignatureUploadModel _ticketSignatureUpload;
        public TicketSignatureUploadModel TicketSignatureUpload
        {
            get { return _ticketSignatureUpload; }
            set { SetProperty(ref _ticketSignatureUpload, value); }
        }

        internal void SaveAsync()
        {
            HotoFiberSession.Current.SetSignatureDetail(TicketSignatureUpload);

            switch (HotoFiberSession.Current.SelectedSignatureId)
            {
                case "1":
                    HotoFiberSession.Current.MemoryStreams.Signature1 = Signature;
                    CheckRequiredFields("1");
                    break;

                case "2":
                    HotoFiberSession.Current.MemoryStreams.Signature2 = Signature;
                    CheckRequiredFields("2");
                    break;

                case "3":
                    HotoFiberSession.Current.MemoryStreams.Signature3 = Signature;
                    CheckRequiredFields("3");
                    break;
            }

        }

        private void CheckRequiredFields(string signId)
        {
            switch (signId)
            {
                case "1":
                    if (HotoFiberSession.Current.MemoryStreams.Signature1 == null)
                    {
                        CommonHelper.DisplayAlertMessage("", "Signature is required.");
                        return;
                    }
                    break;
                case "2":
                    if (string.IsNullOrEmpty(TicketSignatureUpload.CustomerRepresentativeName)
                                    || string.IsNullOrEmpty(TicketSignatureUpload.CustomerRepresentativeMobileNo))
                    {
                        CommonHelper.DisplayAlertMessage("", "Name and Mobile No fields are required");
                        return;
                    }
                    break;

                case "3":
                    if (string.IsNullOrEmpty(TicketSignatureUpload.ThirdPartyRepresentativeName)
                                    || string.IsNullOrEmpty(TicketSignatureUpload.ThirdPartyRepresentativeMobileNo))
                    {
                        CommonHelper.DisplayAlertMessage("", "Name and Mobile No fields are required");
                        return;
                    }
                    break;
            }

            _navigationService.NavigateAsync("SignaturesPage", null, false, false);

        }

        private bool _isSignature1Visited;
        public bool IsSignature1Visited
        {
            get { return _isSignature1Visited; }
            set { SetProperty(ref _isSignature1Visited, value); }
        }

        private bool _isSignature2Visited;
        public bool IsSignature2Visited
        {
            get { return _isSignature2Visited; }
            set { SetProperty(ref _isSignature2Visited, value); }
        }

        private bool _isSignature3Visited;
        public bool IsSignature3Visited
        {
            get { return _isSignature3Visited; }
            set { SetProperty(ref _isSignature3Visited, value); }
        }
        public void InitiatePageAsync()
        {
            TicketSignatureUpload = HotoFiberSession.Current.SignatureDetail;

            switch (HotoFiberSession.Current.SelectedSignatureId)
            {
                case "1":
                    InitiateAllFlags();
                    IsSignature1Visited = true;
                    HotoFiberSession.Current.SignatureFlags.IsSignature1Visited = true;
                    break;

                case "2":
                    InitiateAllFlags();
                    IsSignature2Visited = true;
                    HotoFiberSession.Current.SignatureFlags.IsSignature2Visited = true;
                    break;

                case "3":
                    InitiateAllFlags();
                    IsSignature3Visited = true;
                    HotoFiberSession.Current.SignatureFlags.IsSignature3Visited = true;
                    break;
            }

            HotoFiberSession.Current.SetSignatureFlags(HotoFiberSession.Current.SignatureFlags);
        }

        private void InitiateAllFlags()
        {
            IsSignature1Visited = false;
            IsSignature2Visited = false;
            IsSignature3Visited = false;
        }

        public void SaveToSession()
        {

        }
    }
}
