using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using Prism.Commands;
using Prism.Navigation;
using System.IO;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class SignatureContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IBatteryCommissionService _commissionService;
        public SignatureContentPageViewModel(INavigationService navigationService, IFileUploadService fileUploadService,
            IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _fileUploadService = fileUploadService;
            _commissionService = commissionService;
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));


        private MemoryStream _signature;
        public MemoryStream Signature
        {
            get { return _signature; }
            set { SetProperty(ref _signature, value); }
        }

        public void SaveAsync()
        {
            switch (CurrentSignId)
            {
                case 1:
                    BatteryCommissioningSession.Current.SetSignature1(Signature);

                    BatteryCommissioningSession.Current.SetIsSignature1TakenFilled(true);
                    break;

                case 2:
                    if (string.IsNullOrEmpty(CustomerContactPersonName) || string.IsNullOrEmpty(CustomerContactPersonMobile)
                        || string.IsNullOrEmpty(CustomerContactPersonDesignation) || Signature == null)
                    {
                        CommonHelper.DisplayAlertMessage("", "All fields required.");
                        return;
                    }

                    BatteryCommissioningSession.Current.SetSignature2(Signature);

                    BatteryCommissioningSession.Current.Transaction.CustomerContactPersonName = CustomerContactPersonName;

                    BatteryCommissioningSession.Current.Transaction.CustomerContactPersonMobile = CustomerContactPersonMobile;

                    BatteryCommissioningSession.Current.Transaction.CustomerContactPersonDesignation = CustomerContactPersonDesignation;

                    BatteryCommissioningSession.Current.SetIsSignature2TakenFilled(true);
                    break;
            }

            _navigationService.GoBackAsync(null, false, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        private int _currentSignId;
        public int CurrentSignId
        {
            get { return _currentSignId; }
            set { SetProperty(ref _currentSignId, value); }
        }

        private string _signatureTitle;
        public string SignatureTitle
        {
            get { return _signatureTitle; }
            set { SetProperty(ref _signatureTitle, value); }
        }

        private bool _isCustomerContactPerson;
        public bool IsCustomerContactPerson
        {
            get { return _isCustomerContactPerson; }
            set { SetProperty(ref _isCustomerContactPerson, value); }
        }


        private string _customerContactPersonName;
        public string CustomerContactPersonName
        {
            get { return _customerContactPersonName; }
            set { SetProperty(ref _customerContactPersonName, value); }
        }

        private string _customerContactPersonMobile;
        public string CustomerContactPersonMobile
        {
            get { return _customerContactPersonMobile; }
            set { SetProperty(ref _customerContactPersonMobile, value); }
        }

        private string _customerContactPersonDesignation;
        public string CustomerContactPersonDesignation
        {
            get { return _customerContactPersonDesignation; }
            set { SetProperty(ref _customerContactPersonDesignation, value); }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CurrentSignId = (int)parameters["sign"];

            switch (CurrentSignId)
            {
                case 1:
                    SignatureTitle = "Signature for M&M representative";
                    IsCustomerContactPerson = false;
                    break;

                case 2:
                    SignatureTitle = "Signature Customer’s representative";
                    IsCustomerContactPerson = true;

                    SetFieldsDefaultValueAsync();
                    break;
            }
        }

        private void SetFieldsDefaultValueAsync()
        {
            var transaction = BatteryCommissioningSession.Current.Transaction;

            CustomerContactPersonName = transaction.CustomerContactPersonName;
            CustomerContactPersonMobile = transaction.CustomerContactPersonMobile;
            CustomerContactPersonDesignation = transaction.CustomerContactPersonDesignation;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}
