using MnMLilon.Service.Configuration;
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
    public class BatteryListContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        public BatteryListContentPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }


        private ObservableCollection<Category> _batteryList;
        public ObservableCollection<Category> BatteryList
        {
            get { return _batteryList; }
            set { SetProperty(ref _batteryList, value); }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            BatteryList = new ObservableCollection<Category>();

            var batteryList = BatteryCommissioningSession.Current.Transaction.TransactionBatteryList;

            foreach (var item in batteryList)
            {
                var battery = new Category();

                if (!string.IsNullOrEmpty(item.BatteryName))
                {
                    bool isFormFilled = GetWhichFormFilled(item.SerialNo);
                    if (isFormFilled)
                    {
                        battery.Flag = true;
                    }

                    battery.Id = item.SerialNo;
                    battery.Name = item.BatteryName;
                    battery.Flag2 = item.CurrentCommissioned;

                    battery.BgColor = (item.CurrentCommissioned == false) ? "#050e37" : "#0076a3";

                    BatteryList.Add(battery);
                }
            }
        }


        private bool GetWhichFormFilled(int serialNo)
        {
            //UserSession.Current.SetIsBatteryBankDataInformationFilled(true);
            bool isFilled = false;

            switch (serialNo)
            {
                case 1:
                    isFilled = BatteryCommissioningSession.Current.IsBatteryNo1Filled;
                    break;

                case 2:
                    isFilled = BatteryCommissioningSession.Current.IsBatteryNo2Filled;
                    break;

                case 3:
                    isFilled = BatteryCommissioningSession.Current.IsBatteryNo3Filled;
                    break;

                case 4:
                    isFilled = BatteryCommissioningSession.Current.IsBatteryNo4Filled;
                    break;

                case 5:
                    isFilled = BatteryCommissioningSession.Current.IsBatteryNo5Filled;
                    break;

                case 6:
                    isFilled = BatteryCommissioningSession.Current.IsBatteryNo6Filled;
                    break;

                case 7:
                    isFilled = BatteryCommissioningSession.Current.IsBatteryNo7Filled;
                    break;

                case 8:
                    isFilled = BatteryCommissioningSession.Current.IsBatteryNo8Filled;
                    break;
            }

            return isFilled;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }


        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            BatteryCommissioningSession.Current.SetIsBatteryBankDataInformationFilled(true);

            _navigationService.GoBackAsync(null, false, false);
        }

        public async void NavigateToBatteryBankDataInformationContentPageAsync(Category category)
        {
            var param = new NavigationParameters();
            param.Add("batteryInfo", category);

            await _navigationService.NavigateAsync("BatteryBankDataInformationContentPage", param, false, false);
        }
    }
}
