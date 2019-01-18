using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class SignPageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        public SignPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
                    TroubleTicketSession.Current.SetSignature1(Signature);

                    TroubleTicketSession.Current.SetIsSignature1TakenFilled(true);
                    break;

                case 2:
                    if (string.IsNullOrEmpty(CustomerContactPersonName) || string.IsNullOrEmpty(CustomerContactPersonMobile)
                        || string.IsNullOrEmpty(CustomerContactPersonDesignation) || Signature == null)
                    {
                        CommonHelper.DisplayAlertMessage("", "All fields required.");
                        return;
                    }

                    TroubleTicketSession.Current.SetSignature2(Signature);

                    TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.CustomerRepresentativeName = CustomerContactPersonName;

                    TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.CustomerRepresentativeMobileNo = CustomerContactPersonMobile;

                    TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.CustomerRepresentativeDesignation = CustomerContactPersonDesignation;

                    TroubleTicketSession.Current.SetIsSignature2TakenFilled(true);
                    break;
            }

            _navigationService.NavigateAsync("SignaturePage", null, false, false);
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
            var ticketModel = TroubleTicketSession.Current.TroubleTicket;

            CustomerContactPersonName = ticketModel.TroubleTicketDetails.CustomerRepresentativeName;
            CustomerContactPersonMobile = ticketModel.TroubleTicketDetails.CustomerRepresentativeMobileNo;
            CustomerContactPersonDesignation = ticketModel.TroubleTicketDetails.CustomerRepresentativeDesignation;
        }
    }
}
