using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.DataAccess.Implementation;
using MnMLilon.Service.DataAccess.Model;
using MnMLilon.Service.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class ElectricalInstallationContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _commissionService;
        public ElectricalInstallationContentPageViewModel(INavigationService navigationService, IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _commissionService = commissionService;
        }

        private bool _busBarToBatterPowerConnectionOk;
        public bool BusBarToBatterPowerConnectionOk
        {
            get { return _busBarToBatterPowerConnectionOk; }
            set { SetProperty(ref _busBarToBatterPowerConnectionOk, value); }
        }

        private bool _moduleConnectionOk;
        public bool ModuleConnectionOk
        {
            get { return _moduleConnectionOk; }
            set { SetProperty(ref _moduleConnectionOk, value); }
        }

        private bool _powerPlantConnectionOk;
        public bool PowerPlantConnectionOk
        {
            get { return _powerPlantConnectionOk; }
            set { SetProperty(ref _powerPlantConnectionOk, value); }
        }

        private bool _batteryComConnectionOk;
        public bool BatteryComConnectionOk
        {
            get { return _batteryComConnectionOk; }
            set { SetProperty(ref _batteryComConnectionOk, value); }
        }

        private bool _powerConnectionOk;
        public bool PowerConnectionOk
        {
            get { return _powerConnectionOk; }
            set { SetProperty(ref _powerConnectionOk, value); }
        }

        private string _busBarToBatterPowerConnectionComment;
        public string BusBarToBatterPowerConnectionComment
        {
            get { return _busBarToBatterPowerConnectionComment; }
            set { SetProperty(ref _busBarToBatterPowerConnectionComment, value); }
        }

        private string _moduleConnectionComment;
        public string ModuleConnectionComment
        {
            get { return _moduleConnectionComment; }
            set { SetProperty(ref _moduleConnectionComment, value); }
        }

        private string _powerPlantConnectionComment;
        public string PowerPlantConnectionComment
        {
            get { return _powerPlantConnectionComment; }
            set { SetProperty(ref _powerPlantConnectionComment, value); }
        }

        private string _batteryComConnectionComment;
        public string BatteryComConnectionComment
        {
            get { return _batteryComConnectionComment; }
            set { SetProperty(ref _batteryComConnectionComment, value); }
        }

        private string _powerConnectionComment;
        public string PowerConnectionComment
        {
            get { return _powerConnectionComment; }
            set { SetProperty(ref _powerConnectionComment, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            BatteryCommissioningSession.Current.SetIsElectricalInstallationFilled(true);

            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.BusBarToBatterPowerConnectionOk = BusBarToBatterPowerConnectionOk;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.ModuleConnectionOk = ModuleConnectionOk;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.PowerPlantConnectionOk = PowerPlantConnectionOk;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.BatteryComConnectionOk = BatteryComConnectionOk;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.PowerConnectionOk = PowerConnectionOk;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.BusBarToBatterPowerConnectionComment = BusBarToBatterPowerConnectionComment;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.ModuleConnectionComment = ModuleConnectionComment;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.PowerPlantConnectionComment = PowerPlantConnectionComment;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.BatteryComConnectionComment = BatteryComConnectionComment;
            BatteryCommissioningSession.Current.Transaction.ElectricalInstallationModel.PowerConnectionComment = PowerConnectionComment;

            _navigationService.GoBackAsync(null, false, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetFieldsDefaultValueAsync();
        }

        private void SetFieldsDefaultValueAsync()
        {
            var transaction = BatteryCommissioningSession.Current.Transaction;

            BusBarToBatterPowerConnectionOk = transaction.ElectricalInstallationModel.BusBarToBatterPowerConnectionOk;
            ModuleConnectionOk = transaction.ElectricalInstallationModel.ModuleConnectionOk;
            PowerPlantConnectionOk = transaction.ElectricalInstallationModel.PowerPlantConnectionOk;
            BatteryComConnectionOk = transaction.ElectricalInstallationModel.BatteryComConnectionOk;
            PowerConnectionOk = transaction.ElectricalInstallationModel.PowerConnectionOk;
            BusBarToBatterPowerConnectionComment = transaction.ElectricalInstallationModel.BusBarToBatterPowerConnectionComment;
            ModuleConnectionComment = transaction.ElectricalInstallationModel.ModuleConnectionComment;
            PowerPlantConnectionComment = transaction.ElectricalInstallationModel.PowerPlantConnectionComment;
            BatteryComConnectionComment = transaction.ElectricalInstallationModel.BatteryComConnectionComment;
            PowerConnectionComment = transaction.ElectricalInstallationModel.PowerConnectionComment;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
