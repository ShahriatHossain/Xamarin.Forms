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
    public class BatteryDetailsPageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly ITroubleTicketService _troubleTicketService;
        public BatteryDetailsPageViewModel(INavigationService navigationService, ITroubleTicketService troubleTicketService)
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


        private string _batterySoftwareVersion;
        public string BatterySoftwareVersion
        {
            get { return _batterySoftwareVersion; }
            set { SetProperty(ref _batterySoftwareVersion, value); }
        }

        private ObservableCollection<BatteryCommissionSiteBattery> _batteryList;
        public ObservableCollection<BatteryCommissionSiteBattery> BatteryList
        {
            get { return _batteryList; }
            set { SetProperty(ref _batteryList, value); }
        }
        public void SetDefaultFieldsAsync()
        {
            var batteryList = TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatteryList;

            BatteryList = new ObservableCollection<BatteryCommissionSiteBattery>();

            if (batteryList != null)
            {
                foreach (var item in batteryList)
                {
                    BatteryList.Add(item);
                }
            }

            BatterySoftwareVersion = TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatterySoftwareVersion;
        }

        
        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        public async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsBatteryDetailsFilled(true);

            TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatterySoftwareVersion = BatterySoftwareVersion;

            TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatteryList = new List<BatteryCommissionSiteBattery>();

            foreach (var item in BatteryList)
            {
                TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatteryList.Add(item);
            }

            await _navigationService.NavigateAsync("EquipmentDetailsPage", null, false, false);
        }
    }
}
