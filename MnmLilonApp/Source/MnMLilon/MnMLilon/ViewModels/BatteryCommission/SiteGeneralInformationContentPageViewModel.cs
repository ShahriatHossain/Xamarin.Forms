using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.DataAccess.Implementation;
using MnMLilon.Service.DataAccess.Model;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class SiteGeneralInformationContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _commissionService;
        public SiteGeneralInformationContentPageViewModel(INavigationService navigationService, IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _commissionService = commissionService;

            GetAllCategory();
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

        private string _powerPlantSoftwareVersion;
        public string PowerPlantSoftwareVersion
        {
            get { return _powerPlantSoftwareVersion; }
            set { SetProperty(ref _powerPlantSoftwareVersion, value); }
        }

        private bool _dGDetailsAvailable;
        public bool DGDetailsAvailable
        {
            get { return _dGDetailsAvailable; }
            set { SetProperty(ref _dGDetailsAvailable, value); }
        }

        private bool _eBDetailsAvailable;
        public bool EBDetailsAvailable
        {
            get { return _eBDetailsAvailable; }
            set { SetProperty(ref _eBDetailsAvailable, value); }
        }

        private int _siteConfigId;
        public int SiteConfigId
        {
            get { return _siteConfigId; }
            set { SetProperty(ref _siteConfigId, value); }
        }

        private string _latitude;
        public string Latitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value); }
        }

        private string _longitude;
        public string Longitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            BatteryCommissioningSession.Current.SetIsSiteGeneralInformationFilled(true);

            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.SiteMake = string.IsNullOrEmpty(SiteMake) ? string.Empty : SiteMake.ToUpper();
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.SiteModel = string.IsNullOrEmpty(SiteModel) ? string.Empty : SiteModel.ToUpper();
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.PowerPlantSoftwareVersion = string.IsNullOrEmpty(PowerPlantSoftwareVersion) ? string.Empty : PowerPlantSoftwareVersion;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.SiteCapacity = CommonHelper.GetTextboxValue(SiteCapacity);
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.DGDetailsAvailable = DGDetailsAvailable;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.EBDetailsAvailable = EBDetailsAvailable;

            if(SiteConfigId <= 0)
            {
                CommonHelper.DisplayAlertMessage("", "Site configuration field is required.");
            }
            else if(CommonHelper.GetTextboxValue(Latitude) <= 0 || CommonHelper.GetTextboxValue(Longitude) <= 0)
            {
                CommonHelper.DisplayAlertMessage("", "Latitude and Longitude is required.");
            }
            else
            {
                BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.SiteConfigId = SiteConfigId;

                _navigationService.GoBackAsync(null, false, false);
            }
            
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GetAllCategory();

            SetFieldsDefaultValueAsync();
        }

        private SiteConfig _siteConfigItem;
        public SiteConfig SiteConfigItem
        {
            get { return _siteConfigItem; }
            set { SetProperty(ref _siteConfigItem, value); }
        }

        private void SetFieldsDefaultValueAsync()
        {
            var transaction = BatteryCommissioningSession.Current.Transaction;

            Latitude = CommonHelper.SetTextboxValue(transaction.Latitude.ToString());
            Longitude = CommonHelper.SetTextboxValue(transaction.Longitude.ToString());

            SiteMake = string.IsNullOrEmpty(transaction.TransactionDetailsModel.SiteMake) ? string.Empty: transaction.TransactionDetailsModel.SiteMake.ToUpper();
            SiteModel = string.IsNullOrEmpty(transaction.TransactionDetailsModel.SiteModel) ? string.Empty: transaction.TransactionDetailsModel.SiteModel.ToUpper();
            PowerPlantSoftwareVersion = string.IsNullOrEmpty(transaction.TransactionDetailsModel.PowerPlantSoftwareVersion) ? string.Empty : transaction.TransactionDetailsModel.PowerPlantSoftwareVersion;

            SiteCapacity = CommonHelper.SetTextboxValue(transaction.TransactionDetailsModel.SiteCapacity.ToString());
            DGDetailsAvailable = transaction.TransactionDetailsModel.DGDetailsAvailable;
            EBDetailsAvailable = transaction.TransactionDetailsModel.EBDetailsAvailable;
            SiteConfigId = Convert.ToInt32(transaction.TransactionDetailsModel.SiteConfigId);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }


        private ObservableCollection<SiteConfig> _siteConfigList;
        public ObservableCollection<SiteConfig> SiteConfigList
        {
            get { return _siteConfigList; }
            set { SetProperty(ref _siteConfigList, value); }
        }
        private async void GetAllCategory()
        {
            SiteConfigList = await _commissionService.GetAllSiteConfigAsync();
        }

    }
}
