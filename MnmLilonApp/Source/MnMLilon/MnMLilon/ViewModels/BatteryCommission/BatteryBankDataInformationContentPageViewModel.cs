using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Navigation;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class BatteryBankDataInformationContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _commissionService;
        public BatteryBankDataInformationContentPageViewModel(INavigationService navigationService, IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _commissionService = commissionService;
        }

        private int _serialNo;
        public int SerialNo
        {
            get { return _serialNo; }
            set { SetProperty(ref _serialNo, value); }
        }

        private string _packVoltage;
        public string PackVoltage
        {
            get { return _packVoltage; }
            set { SetProperty(ref _packVoltage, value); }
        }

        private string _socPercent;
        public string SocPercent
        {
            get { return _socPercent; }
            set { SetProperty(ref _socPercent, value); }
        }

        private string _sohPercent;
        public string SohPercent
        {
            get { return _sohPercent; }
            set { SetProperty(ref _sohPercent, value); }
        }

        private string _cellVoltageMin;
        public string CellVoltageMin
        {
            get { return _cellVoltageMin; }
            set { SetProperty(ref _cellVoltageMin, value); }
        }

        private string _cellVoltageMax;
        public string CellVoltageMax
        {
            get { return _cellVoltageMax; }
            set { SetProperty(ref _cellVoltageMax, value); }
        }

        private string _cellTempMin;
        public string CellTempMin
        {
            get { return _cellTempMin; }
            set { SetProperty(ref _cellTempMin, value); }
        }

        private string _cellTempMax;
        public string CellTempMax
        {
            get { return _cellTempMax; }
            set { SetProperty(ref _cellTempMax, value); }
        }

        private string _mosfetTemp;
        public string MosfetTemp
        {
            get { return _mosfetTemp; }
            set { SetProperty(ref _mosfetTemp, value); }
        }

        private string _currentCommissioned;
        public string CurrentCommissioned
        {
            get { return _currentCommissioned; }
            set { SetProperty(ref _currentCommissioned, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            SetIsFormFilled();

            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).BatteryName = BatteryName;
            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).PackVoltage = CommonHelper.GetTextboxValue(PackVoltage);
            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).SocPercent = CommonHelper.GetTextboxValue(SocPercent);
            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).SohPercent = CommonHelper.GetTextboxValue(SohPercent);
            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).CellVoltageMin = CommonHelper.GetTextboxValue(CellVoltageMin);
            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).CellVoltageMax = CommonHelper.GetTextboxValue(CellVoltageMax);
            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).CellTempMin = CommonHelper.GetTextboxValue(CellTempMin);
            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).CellTempMax = CommonHelper.GetTextboxValue(CellTempMax);
            BatteryCommissioningSession.Current.Transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo).MosfetTemp = CommonHelper.GetTextboxValue(MosfetTemp);
            
            _navigationService.GoBackAsync(null, false, false);
        }

        private void SetIsFormFilled()
        {
            //UserSession.Current.SetIsBatteryBankDataInformationFilled(true);

            switch (SerialNo)
            {
                case 1:
                    BatteryCommissioningSession.Current.SetIsBatteryNo1Filled(true);
                    break;

                case 2:
                    BatteryCommissioningSession.Current.SetIsBatteryNo2Filled(true);
                    break;

                case 3:
                    BatteryCommissioningSession.Current.SetIsBatteryNo3Filled(true);
                    break;

                case 4:
                    BatteryCommissioningSession.Current.SetIsBatteryNo4Filled(true);
                    break;

                case 5:
                    BatteryCommissioningSession.Current.SetIsBatteryNo5Filled(true);
                    break;

                case 6:
                    BatteryCommissioningSession.Current.SetIsBatteryNo6Filled(true);
                    break;

                case 7:
                    BatteryCommissioningSession.Current.SetIsBatteryNo7Filled(true);
                    break;

                case 8:
                    BatteryCommissioningSession.Current.SetIsBatteryNo8Filled(true);
                    break;
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        private string _batteryName;
        public string BatteryName
        {
            get { return _batteryName; }
            set { SetProperty(ref _batteryName, value); }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var batteryInfo = (Category)parameters["batteryInfo"];

            SerialNo = batteryInfo.Id;
            BatteryName = batteryInfo.Name;

            SetFieldsDefaultValueAsync();
        }


        private int _serverBatteryId;
        public int ServerBatteryId
        {
            get { return _serverBatteryId; }
            set { SetProperty(ref _serverBatteryId, value); }
        }

        private void SetFieldsDefaultValueAsync()
        {
            var transaction = BatteryCommissioningSession.Current.Transaction;

            var batteryInfo = transaction.TransactionBatteryList.Find(x => x.SerialNo == SerialNo);
            if (batteryInfo != null)
            {
                PackVoltage = CommonHelper.SetTextboxValue(batteryInfo.PackVoltage.ToString());
                SocPercent = CommonHelper.SetTextboxValue(batteryInfo.SocPercent.ToString());
                SohPercent = CommonHelper.SetTextboxValue(batteryInfo.SohPercent.ToString());
                CellVoltageMin = CommonHelper.SetTextboxValue(batteryInfo.CellVoltageMin.ToString());
                CellVoltageMax = CommonHelper.SetTextboxValue(batteryInfo.CellVoltageMax.ToString());
                CellTempMin = CommonHelper.SetTextboxValue(batteryInfo.CellTempMin.ToString());
                CellTempMax = CommonHelper.SetTextboxValue(batteryInfo.CellTempMax.ToString());
                MosfetTemp = CommonHelper.SetTextboxValue(batteryInfo.MosfetTemp.ToString());
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}
