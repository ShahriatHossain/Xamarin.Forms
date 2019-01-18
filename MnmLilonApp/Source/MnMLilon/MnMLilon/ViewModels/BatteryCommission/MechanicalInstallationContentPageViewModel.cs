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
    public class MechanicalInstallationContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _commissionService;
        public MechanicalInstallationContentPageViewModel(INavigationService navigationService, IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _commissionService = commissionService;
        }

        private bool _busBarMounting;
        public bool BusBarMounting
        {
            get { return _busBarMounting; }
            set { SetProperty(ref _busBarMounting, value); }
        }

        private string _busBarMountingComment;
        public string BusBarMountingComment
        {
            get { return _busBarMountingComment; }
            set { SetProperty(ref _busBarMountingComment, value); }
        }

        private bool _batteryMounting;
        public bool BatteryMounting
        {
            get { return _batteryMounting; }
            set { SetProperty(ref _batteryMounting, value); }
        }

        private string _batteryMountingComment;
        public string BatteryMountingComment
        {
            get { return _batteryMountingComment; }
            set { SetProperty(ref _batteryMountingComment, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            BatteryCommissioningSession.Current.SetIsMechanicalInstallationFilled(true);

            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.BusBarMounting = BusBarMounting;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.BusBarMountingComment = BusBarMountingComment;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.BatteryMounting = BatteryMounting;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.BatteryMountingComment = BatteryMountingComment;

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

            BusBarMounting = transaction.TransactionDetailsModel.BusBarMounting;
            BusBarMountingComment = transaction.TransactionDetailsModel.BusBarMountingComment;
            BatteryMounting = transaction.TransactionDetailsModel.BatteryMounting;
            BatteryMountingComment = transaction.TransactionDetailsModel.BatteryMountingComment;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
