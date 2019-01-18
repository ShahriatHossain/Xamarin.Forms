using MnMLilon.Service.Configuration;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class FunctionalTestOnBatteryPageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        public FunctionalTestOnBatteryPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsFunctionalTestonBatteryFilled(true);

            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.VisualInspection = VisualInspection;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.CommunicationTest = CommunicationTest;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.FunctionalTest = FunctionalTest;

            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.VisualInspectionRemarks = VisualInspectionRemarks;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.CommunicationRemarks = CommunicationRemarks;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.FunctionalTestRemarks = FunctionalTestRemarks;

            await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetDefaultFields();
        }

        private bool _visualInspection;
        public bool VisualInspection
        {
            get { return _visualInspection; }
            set { SetProperty(ref _visualInspection, value); }
        }

        private bool _communicationTest;
        public bool CommunicationTest
        {
            get { return _communicationTest; }
            set { SetProperty(ref _communicationTest, value); }
        }

        private bool _functionalTest;
        public bool FunctionalTest
        {
            get { return _functionalTest; }
            set { SetProperty(ref _functionalTest, value); }
        }

        private string _visualInspectionRemarks;
        public string VisualInspectionRemarks
        {
            get { return _visualInspectionRemarks; }
            set { SetProperty(ref _visualInspectionRemarks, value); }
        }

        private string _communicationRemarks;
        public string CommunicationRemarks
        {
            get { return _communicationRemarks; }
            set { SetProperty(ref _communicationRemarks, value); }
        }

        private string _functionalTestRemarks;
        public string FunctionalTestRemarks
        {
            get { return _functionalTestRemarks; }
            set { SetProperty(ref _functionalTestRemarks, value); }
        }
        private void SetDefaultFields()
        {
            var ticketModel = TroubleTicketSession.Current.TroubleTicket;

            VisualInspection = ticketModel.TroubleTicketDetails.VisualInspection;
            CommunicationTest = ticketModel.TroubleTicketDetails.CommunicationTest;
            FunctionalTest = ticketModel.TroubleTicketDetails.FunctionalTest;

            VisualInspectionRemarks = ticketModel.TroubleTicketDetails.VisualInspectionRemarks;
            CommunicationRemarks = ticketModel.TroubleTicketDetails.CommunicationRemarks;
            FunctionalTestRemarks = ticketModel.TroubleTicketDetails.FunctionalTestRemarks;
        }
    }
}
