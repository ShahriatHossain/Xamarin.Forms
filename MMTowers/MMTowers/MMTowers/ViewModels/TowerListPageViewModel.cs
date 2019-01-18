using MMTowers.Helper;
using MMTowers.Service.Common;
using MMTowers.Service.Interface;
using MMTowers.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace MMTowers.ViewModels
{
    public class TowerListPageViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Tower> _towerList;
        public ObservableCollection<Tower> TowerList
        {
            get { return _towerList; }
            set { SetProperty(ref _towerList, value); }
        }

        private readonly INavigationService _navigationService;
        private readonly ITowerService _towerService;

        public TowerListPageViewModel(INavigationService navigationService, ITowerService towerService)
        {
            _navigationService = navigationService;
            _towerService = towerService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!CommonHelper.HasInternetConnection())
            {
                CommonHelper.DisplayAlertMessage("", "Please check your internet connection.");
                return;
            }
            else
            {
                GetAllTowers();
            }

            //CheckDeviceLocationEnabled();

            CheckRequiredPermissionEnabled();
        }

        private void CheckRequiredPermissionEnabled()
        {
            CommonHelper.EnableAllRequiredDevicePermissions();
        }

        internal async void GetAllTowers()
        {
            var userName = UserSession.Current.UserName;
            TowerList = await _towerService.GetAllByTechnicianAsync(userName);
        }

        private async void CheckDeviceLocationEnabled()
        {
            var isLocaltionEnabled = await CommonHelper.IsLocationEnabledFromDevice();
            if (!isLocaltionEnabled)
            {
                CommonHelper.DisplayAlertMessage("", "Please enable device location.");
                return;
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public async void NavigateToTowerDetailAsync(Tower tower)
        {
            try
            {
                await CommonHelper.ShowLoaderAsync();

                var Latitude = (tower.Latitude == null) ? 0.0 : (Double)tower.Latitude;
                var Longitude = (tower.Longitude == null) ? 0.0 : (Double)tower.Longitude;
                var isLocationEnabled = await CommonHelper.IsLocationEnabledFromDevice();

                if (!isLocationEnabled)
                {
                    await CommonHelper.HideLoaderAsync();

                    CommonHelper.DisplayAlertMessage("", "Please enable your device location.");
                    return;
                }
                else
                {
                    var isUserInRange = await CommonHelper.IsUserInDesiredLocation(Latitude, Longitude);

                    var location = await CommonHelper.GetDeviceCurrentLocation();

                    await CommonHelper.HideLoaderAsync();

                    if (!isUserInRange || location == null)
                    {
                        CommonHelper.DisplayAlertMessage("", "You are not in the desired location and your current device location is Latitude: " + location.Latitude + "\nLongitude: " + location.Longitude + " \n and server location is: \nLatitude: " + Latitude + "\nLongitude: " + Longitude);
                        return;
                    }
                    else
                    {
                        var param = new NavigationParameters();
                        param.Add("btsId", tower.Id);
                        await _navigationService.NavigateAsync("DieselRefillPage", param, false, false);
                    }
                }

            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();
            }

        }

        DelegateCommand _settingsCommand;
        public DelegateCommand SettingsCommand => _settingsCommand != null ?
            _settingsCommand : (_settingsCommand = new DelegateCommand(NavigateToSettingAsync));

        private async void NavigateToSettingAsync()
        {
            await _navigationService.NavigateAsync("SettingPage", null, false, false);
        }

        DelegateCommand _changePasswordCommand;
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand != null ?
            _changePasswordCommand : (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));

        private async void ChangePasswordAsync()
        {
            await _navigationService.NavigateAsync("PasswordChangePage", null, false, false);
        }

        DelegateCommand _reLoginCommand;
        public DelegateCommand ReLoginCommand => _reLoginCommand != null ?
            _reLoginCommand : (_reLoginCommand = new DelegateCommand(ReLoginAsync));

        private async void ReLoginAsync()
        {
            DatabaseHelper.DeleteAllUsers();
            await _navigationService.NavigateAsync("LoginPage", null, false, false);
        }


        DelegateCommand _currentLocationCommand;
        public DelegateCommand CurrentLocationCommand => _currentLocationCommand != null ?
            _currentLocationCommand : (_currentLocationCommand = new DelegateCommand(GetCurrentLocationAsync));

        private async void GetCurrentLocationAsync()
        {
            try
            {
                await CommonHelper.ShowLoaderAsync();

                var isLocationEnabled = await CommonHelper.IsLocationEnabledFromDevice();

                if (!isLocationEnabled)
                {
                    await CommonHelper.HideLoaderAsync();

                    CommonHelper.DisplayAlertMessage("", "Please enable your device location.");
                    return;
                }

                var location = await CommonHelper.GetDeviceCurrentLocation();

                await CommonHelper.HideLoaderAsync();

                if (location != null)
                {
                    CommonHelper.DisplayAlertMessage("", "Latitude: " + location.Latitude + "\nLongitude: " + location.Longitude);
                }
            }
            catch (Exception ex)
            {

                await CommonHelper.HideLoaderAsync();

                CommonHelper.DisplayAlertMessage("", "Failed to get device location.");
            }

        }
    }
}
