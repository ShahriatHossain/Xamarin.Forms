using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class BatteryGeneralInformationContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _commissionService;
        public BatteryGeneralInformationContentPageViewModel(INavigationService navigationService, IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _commissionService = commissionService;
        }
        
        
        private string _batteryModelNo;
        public string BatteryModelNo
        {
            get { return _batteryModelNo; }
            set { SetProperty(ref _batteryModelNo, value); }
        }

        private string _batterySoftwareVersion;
        public string BatterySoftwareVersion
        {
            get { return _batterySoftwareVersion; }
            set { SetProperty(ref _batterySoftwareVersion, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            if(string.IsNullOrEmpty(BatteryModelNo) || string.IsNullOrEmpty(BatterySoftwareVersion))
            {
                CommonHelper.DisplayAlertMessage("", "Model No and Software Version Fields are required.");
            }
            else
            {
                BatteryCommissioningSession.Current.SetIsBatteryGeneralInformationFilled(true);

                BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.BatteryModelNo = BatteryModelNo;
                BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.BatterySoftwareVersion = BatterySoftwareVersion;

                BatteryCommissioningSession.Current.Transaction.TransactionBatteryList = new List<TransactionBatteryDetail>();
                foreach (var item in BatteryList)
                {
                    BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Add(item);
                }

                _navigationService.GoBackAsync(null, false, false);
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }
        
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetFieldsDefaultValueAsync();
        }


        private ObservableCollection<TransactionBatteryDetail> _batteryList;
        public ObservableCollection<TransactionBatteryDetail> BatteryList
        {
            get { return _batteryList; }
            set { SetProperty(ref _batteryList, value); }
        }

        private void SetFieldsDefaultValueAsync()
        {
            var transaction = BatteryCommissioningSession.Current.Transaction;
            BatteryModelNo = transaction.TransactionDetailsModel.BatteryModelNo;
            BatterySoftwareVersion = transaction.TransactionDetailsModel.BatterySoftwareVersion;

            BatteryList = new ObservableCollection<TransactionBatteryDetail>();

            foreach (var item in transaction.TransactionBatteryList)
            {
                BatteryList.Add(item);
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}
