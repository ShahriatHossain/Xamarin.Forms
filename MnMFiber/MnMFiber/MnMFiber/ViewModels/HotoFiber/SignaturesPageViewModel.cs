using MnMFiber.Common.Helper;
using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class SignaturesPageViewModel : BindableBase, IBaseInterface
    {
        private readonly INavigationService _navigationService;
        private readonly IHotoFiberService _hotoFiberRepository;

        public SignaturesPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
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

        private SignatureFlagsDto _signatureFlags;
        public SignatureFlagsDto SignatureFlags
        {
            get { return _signatureFlags; }
            set { SetProperty(ref _signatureFlags, value); }
        }

        public void InitiatePageAsync()
        {
            SignatureFlags = HotoFiberSession.Current.SignatureFlags;

            CheckAllTabsFilledUp();
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

            if (HotoFiberSession.Current.SignatureFlags.IsSignature1Visited == true
                && (HotoFiberSession.Current.SignatureFlags.IsSignature2Visited == true
                || HotoFiberSession.Current.SignatureFlags.IsSignature3Visited == true))
            {
                IsAllTabsFilledUp = true;
            }

        }

        public void SaveToSession()
        {

        }


        DelegateCommand<string> _signatureCommand;
        public DelegateCommand<string> SignatureCommand => _signatureCommand != null ?
            _signatureCommand : (_signatureCommand = new DelegateCommand<string>(GotoSignatureDetailPage));

        private void GotoSignatureDetailPage(string id)
        {
            HotoFiberSession.Current.SetSelectedSignatureId(id);
            _navigationService.NavigateAsync("SignatureDetailPage", null, false, false);
        }


        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            await CommonHelper.ShowLoaderAsync();
            await SetSignaturesAsync();

            HotoFiberSession.Current.SignatureDetail.TicketId = HotoFiberSession.Current.TicketBasic.TicketId;

            if (!string.IsNullOrEmpty(HotoFiberSession.Current.SignatureDetail.PatrollerSignatureFileName)
                && (!string.IsNullOrEmpty(HotoFiberSession.Current.SignatureDetail.CustomerRepresentativeFileName)
                || !string.IsNullOrEmpty(HotoFiberSession.Current.SignatureDetail.ThirdPartyRepresentativeFileName)))
            {
                var response = await _hotoFiberRepository.SubmitTicketAsComplete(HotoFiberSession.Current.SignatureDetail);

                if (response != null && response.Success)
                {
                    await _navigationService.NavigateAsync("HotoTicketListPage", null, false, false);

                    await CommonHelper.HideLoaderAsync();

                    CommonHelper.DisplayAlertMessage("", "Successfully completed hoto.");
                }
                else
                {
                    await CommonHelper.HideLoaderAsync();

                    CommonHelper.DisplayAlertMessage("", "Something went wrong, please try again.");
                }
            }
            else
            {
                await CommonHelper.HideLoaderAsync();

                CommonHelper.DisplayAlertMessage("", "Signatures uploaded failed, please try again.");
            }

        }

        private async System.Threading.Tasks.Task SetSignaturesAsync()
        {
            if (HotoFiberSession.Current.MemoryStreams.Signature1 != null)
            {
                var signature1 = await _hotoFiberRepository
                .UploadFileAsync(HotoFiberSession.Current.MemoryStreams.Signature1);
                HotoFiberSession.Current.SignatureDetail.PatrollerSignatureFileName = signature1;
                HotoFiberSession.Current.SignatureDetail.PatrollerSignatureFileDisplayName = signature1;
            }

            if (HotoFiberSession.Current.MemoryStreams.Signature2 != null)
            {
                var signature2 = await _hotoFiberRepository
                .UploadFileAsync(HotoFiberSession.Current.MemoryStreams.Signature2);
                HotoFiberSession.Current.SignatureDetail.CustomerRepresentativeFileName = signature2;
                HotoFiberSession.Current.SignatureDetail.CustomerRepresentativeFileDisplayName = signature2;
            }

            if (HotoFiberSession.Current.MemoryStreams.Signature3 != null)
            {
                var signature3 = await _hotoFiberRepository
                .UploadFileAsync(HotoFiberSession.Current.MemoryStreams.Signature3);
                HotoFiberSession.Current.SignatureDetail.ThirdPartyRepresentativeFileName = signature3;
                HotoFiberSession.Current.SignatureDetail.ThirdPartyRepresentativeFileDisplayName = signature3;
            }
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand != null ?
            _cancelCommand : (_cancelCommand = new DelegateCommand(CancelAsync));

        private async void CancelAsync()
        {
            RemoveRelatedSessions();

            await _navigationService.NavigateAsync("HotoTicketDetailPage", null, false, false);
        }

        private void RemoveRelatedSessions()
        {
            HotoFiberSession.Current.SetSignatureFlags(new SignatureFlagsDto { });
            HotoFiberSession.Current.SetMemoryStreams(new MemoryStreamDto { });
            HotoFiberSession.Current.SetSignatureDetail(new TicketSignatureUploadModel { });
        }
    }
}
