using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class PowerPlantOtherDetailsPageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly ITroubleTicketService _troubleTicketService;
        public PowerPlantOtherDetailsPageViewModel(INavigationService navigationService, ITroubleTicketService troubleTicketService)
        {
            _navigationService = navigationService;
            _troubleTicketService = troubleTicketService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetDefaultFieldsAsync();
        }


        private string _chargingVoltage;
        public string ChargingVoltage
        {
            get { return _chargingVoltage; }
            set { SetProperty(ref _chargingVoltage, value); }
        }

        private string _maxChargingModule;
        public string MaxChargingModule
        {
            get { return _maxChargingModule; }
            set { SetProperty(ref _maxChargingModule, value); }
        }

        private string _lvdSettings;
        public string LvdSettings
        {
            get { return _lvdSettings; }
            set { SetProperty(ref _lvdSettings, value); }
        }

        private string _siteMake;
        public string SiteMake
        {
            get { return _siteMake; }
            set { SetProperty(ref _siteMake, value); }
        }

        private string _siteModel;
        public string SiteModel
        {
            get { return _siteModel; }
            set { SetProperty(ref _siteModel, value); }
        }

        private string _siteCapacity;
        public string SiteCapacity
        {
            get { return _siteCapacity; }
            set { SetProperty(ref _siteCapacity, value); }
        }

        private string _siteComments;
        public string SiteComments
        {
            get { return _siteComments; }
            set { SetProperty(ref _siteComments, value); }
        }

        private string _powerPlantSoftwareVersion;
        public string PowerPlantSoftwareVersion
        {
            get { return _powerPlantSoftwareVersion; }
            set { SetProperty(ref _powerPlantSoftwareVersion, value); }
        }

        private async void SetDefaultFieldsAsync()
        {
            var ticketModel = TroubleTicketSession.Current.TroubleTicket;

            var troubleTicketSiteId = TroubleTicketSession.Current.TroubleTicketSiteId;

            var siteModel = await _troubleTicketService.GetSiteDetails(troubleTicketSiteId);
            if (siteModel != null)
            {
                SiteMake = string.Format(@"Make: {0}", siteModel.SiteMake);
                SiteModel = string.Format(@"Model: {0}", siteModel.SiteModel);
                SiteCapacity = string.Format(@"Capacity: {0}", siteModel.SiteCapacity);
            }

            SiteComments = ticketModel.TroubleTicketDetails.SiteComments;
            ChargingVoltage = ticketModel.TroubleTicketDetails.ChargingVoltage;
            MaxChargingModule = ticketModel.TroubleTicketDetails.MaxChargingModule;
            LvdSettings = ticketModel.TroubleTicketDetails.LvdSettings;
            PowerPlantSoftwareVersion = ticketModel.SiteDetails.PowerPlantSoftwareVersion;
        }


        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsPowerPlantNOtherDetailsFilled(true);

            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.ChargingVoltage = ChargingVoltage;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.MaxChargingModule = MaxChargingModule;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.LvdSettings = LvdSettings;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.SiteComments = SiteComments;
            TroubleTicketSession.Current.TroubleTicket.SiteDetails.PowerPlantSoftwareVersion = PowerPlantSoftwareVersion;

            await _navigationService.NavigateAsync("EquipmentDetailsPage", null, false, false);
        }
    }
}
