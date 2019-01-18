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
    public class SiteStatusContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _commissionService;
        public SiteStatusContentPageViewModel(INavigationService navigationService, IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _commissionService = commissionService;
        }


        private bool _siteReady;
        public bool SiteReady
        {
            get { return _siteReady; }
            set { SetProperty(ref _siteReady, value); }
        }

        private bool _materialShortage;
        public bool MaterialShortage
        {
            get { return _materialShortage; }
            set { SetProperty(ref _materialShortage, value); }
        }

        private bool _materialDamaged;
        public bool MaterialDamaged
        {
            get { return _materialDamaged; }
            set { SetProperty(ref _materialDamaged, value); }
        }

        private string _siteReadyRemarks;
        public string SiteReadyRemarks
        {
            get { return _siteReadyRemarks; }
            set { SetProperty(ref _siteReadyRemarks, value); }
        }

        private string _materialShortageRemarks;
        public string MaterialShortageRemarks
        {
            get { return _materialShortageRemarks; }
            set { SetProperty(ref _materialShortageRemarks, value); }
        }

        private string _materialDamagedRemarks;
        public string MaterialDamagedRemarks
        {
            get { return _materialDamagedRemarks; }
            set { SetProperty(ref _materialDamagedRemarks, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            BatteryCommissioningSession.Current.SetIsSiteStatusFilled(true);
            
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.SiteReady = SiteReady;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.MaterialShortage = MaterialShortage;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.MaterialDamaged = MaterialDamaged;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.SiteReadyRemarks = SiteReadyRemarks;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.MaterialShortageRemarks = MaterialShortageRemarks;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.MaterialDamagedRemarks = MaterialDamagedRemarks;

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

            SiteReady = transaction.TransactionDetailsModel.SiteReady;
            MaterialShortage = transaction.TransactionDetailsModel.MaterialShortage;
            MaterialDamaged = transaction.TransactionDetailsModel.MaterialDamaged;
            SiteReadyRemarks = transaction.TransactionDetailsModel.SiteReadyRemarks;
            MaterialShortageRemarks = transaction.TransactionDetailsModel.MaterialShortageRemarks;
            MaterialDamagedRemarks = transaction.TransactionDetailsModel.MaterialDamagedRemarks;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}
