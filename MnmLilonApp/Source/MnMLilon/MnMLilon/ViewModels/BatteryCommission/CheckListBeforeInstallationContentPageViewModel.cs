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
    public class CheckListBeforeInstallationContentPageViewModel : BaseViewModel, INavigationAware
    {   
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _commissionService;
        public CheckListBeforeInstallationContentPageViewModel(INavigationService navigationService, IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _commissionService = commissionService;
        }

        private string _smpsChargingVoltage;
        public string SmpsChargingVoltage
        {
            get { return _smpsChargingVoltage; }
            set { SetProperty(ref _smpsChargingVoltage, value); }
        }

        private bool _batteryModuleOffMode;
        public bool BatteryModuleOffMode
        {
            get { return _batteryModuleOffMode; }
            set { SetProperty(ref _batteryModuleOffMode, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            BatteryCommissioningSession.Current.SetIsCheckListBeforeInstallationFilled(true);

            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.SmpsChargingVoltage = CommonHelper.GetTextboxValue(SmpsChargingVoltage);
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.BatteryModuleOffMode = BatteryModuleOffMode;

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

            SmpsChargingVoltage = CommonHelper.SetTextboxValue(transaction.TransactionDetailsModel.SmpsChargingVoltage.ToString());
            BatteryModuleOffMode = transaction.TransactionDetailsModel.BatteryModuleOffMode;

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}
