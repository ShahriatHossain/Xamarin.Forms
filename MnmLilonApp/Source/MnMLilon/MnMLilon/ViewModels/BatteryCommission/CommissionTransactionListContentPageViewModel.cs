using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class CommissionTransactionListContentPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _batteryCommissionService;
        public CommissionTransactionListContentPageViewModel(INavigationService navigationService,
            IBatteryCommissionService batteryCommissionService)
        {
            _navigationService = navigationService;
            _batteryCommissionService = batteryCommissionService;
        }


        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GetAllCommissionTransactionsAsync();

            BatteryCommissioningSession.Current.SetIsNewMessageArrived(false);

            CheckDeviceLocationEnabled();
        }

        private async void CheckDeviceLocationEnabled()
        {
            var isLocaltionEnabled = await CommonHelper.IsLocationEnabledFromDevice();
            if (!isLocaltionEnabled)
            {
                CommonHelper.DisplayAlertMessage("", "Please enable device location.");
            }
        }


        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }


        private ObservableCollection<TechnicianTransaction> _commissionTransactionList;
        public ObservableCollection<TechnicianTransaction> CommissionTransactionList
        {
            get { return _commissionTransactionList; }
            set { SetProperty(ref _commissionTransactionList, value); }
        }
        private async void GetAllCommissionTransactionsAsync()
        {
            try
            {
                await CommonHelper.ShowLoaderAsync();

                var technicianNumber = UserSession.Current.UserName;

                CommissionTransactionList = new ObservableCollection<TechnicianTransaction>();

                var transactionList = await _batteryCommissionService.GetAllTransactionsAsync(technicianNumber);

                foreach (var item in transactionList)
                {
                    item.RowBackgroundColor = item.ReAssignedToTechnician ? "#050e37" : "#0076a3";
                    CommissionTransactionList.Add(item);
                }

                await CommonHelper.HideLoaderAsync();
            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();
            }
        }

        public async void NavigateToSiteDetailFormListAsync(TechnicianTransaction technicianTransaction)
        {
            await CommonHelper.ShowLoaderAsync();

            BatteryCommissioningSession.Current.SetCommissionTransactionId(Convert.ToInt32(technicianTransaction.Id));
            BatteryCommissioningSession.Current.SetCommissionTransactionNo(technicianTransaction.TransactionNo);
            BatteryCommissioningSession.Current.SetReAssignedToTechnician(technicianTransaction.ReAssignedToTechnician);

            var transaction = await _batteryCommissionService.GetTransactionAsync(Convert.ToInt32(technicianTransaction.Id));
            BatteryCommissioningSession.Current.SetTransaction(transaction);

            if (technicianTransaction.ReAssignedToTechnician)
            {
                var Latitude = (transaction.Latitude == null) ? 0.0 : (Double)transaction.Latitude;
                var Longitude = (transaction.Longitude == null) ? 0.0 : (Double)transaction.Longitude;

                CheckUserInCorrectLocation(Latitude, Longitude);
            }
            else
            {
                if (await IsLocationEnabled())
                {
                    await SetDefaultLatAndLong();

                    await _navigationService.NavigateAsync("SiteDetailFormListContentPage", null, false, false);
                }
            }

            await CommonHelper.HideLoaderAsync();
        }

        private static async System.Threading.Tasks.Task SetDefaultLatAndLong()
        {
            var position = await CommonHelper.GetDeviceCurrentLocation();
            if (position != null)
            {
                BatteryCommissioningSession.Current.Transaction.Latitude = (decimal?)position.Latitude;
                BatteryCommissioningSession.Current.Transaction.Longitude = (decimal?)position.Longitude;
            }
        }

        private static async System.Threading.Tasks.Task<bool> IsLocationEnabled()
        {
            var isLocaltionEnabled = await CommonHelper.IsLocationEnabledFromDevice();
            if (isLocaltionEnabled)
            {
                return true;
            }
            else
            {
                CommonHelper.DisplayAlertMessage("", "Please enable device location.");
                return false;
            }
        }

        private async void CheckUserInCorrectLocation(double latitude, double longitude)
        {
            if (await IsLocationEnabled())
            {
                //if (latitude > 0 || longitude > 0)
                //{
                //    var isUserInRange = await CommonHelper.IsUserInDesiredLocation(latitude, longitude);
                //    var location = await CommonHelper.GetDeviceCurrentLocation();
                //    if (!isUserInRange)
                //    {
                //        CommonHelper.DisplayAlertMessage("", "You are not in the desired location and your current device location is Latitude: " + location.Latitude + "\nLongitude: " + location.Longitude + " \n and server location is: \nLatitude: " + latitude + "\nLongitude: " + longitude);
                //        return;
                //    }
                //}

                await SetDefaultLatAndLong();

                await _navigationService.NavigateAsync("SiteDetailFormListContentPage", null, false, false);
            }
        }


        DelegateCommand _navToHomeCommand;
        public DelegateCommand NavToHomeCommand => _navToHomeCommand ?? (_navToHomeCommand = new DelegateCommand(GoToHome));
        private async void GoToHome()
        {
            await _navigationService.NavigateAsync("CategoryContentPage", null, false, false);
        }
    }
}
