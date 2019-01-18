using Pigeon.Services.Interface;
using Pigeon.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pigeon.Helpers
{
    public class CommonHelper
    {
        public CommonHelper() { }
        public ObservableCollection<T> ConvertIEnumToObservable<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }

        public bool HasNetwork()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            return networkConnection.IsConnected;
        }

        public async void DisplayAlertMessage(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public static async void DisplayAlertMessage(string title, string message, string status)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public void SubscribeChannelForNotification(string channelId)
        {
            var notificationService = DependencyService.Get<IPushNotificationService>();
            notificationService.SubscribeChannelForNotification(channelId);
        }

        public void UnSubscribeChannelForNotification(string channelId)
        {
            var notificationService = DependencyService.Get<IPushNotificationService>();
            notificationService.UnSubscribeChannelForNotification(channelId);
        }

        public async Task ShowLoaderAsync()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopupPage());
            }
            catch (Exception ex)
            {

            }
        }

        public async Task HideLoaderAsync()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public static async void EnableAllRequiredDevicePermissions()
        {
            await IsFileStorageSupportedAsync();

            await IsCameraSupportedAsync();

            await IsTakePhotoSupportedAsync();

            //await IsLocationSupportedAsync();
        }

        public static async Task IsFileStorageSupportedAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        DisplayAlertMessage("", "Please enable your device storage permission", string.Empty);
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                        status = results[Permission.Storage];
                }


            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Please enable your device storage permission", string.Empty);
            }
        }

        public static async Task IsTakePhotoSupportedAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Photos))
                    {
                        DisplayAlertMessage("", "Please enable your device photos permission", string.Empty);
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Photos))
                        status = results[Permission.Photos];
                }


            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Please enable your device photos permission", string.Empty);
            }
        }

        public static async Task IsLocationSupportedAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        DisplayAlertMessage("", "Please enable your device location permission", string.Empty);
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }


            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Please enable your device location permission", string.Empty);
            }
        }

        public static async Task IsCameraSupportedAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        DisplayAlertMessage("", "Please enable your device camera permission", string.Empty);
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }


            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Please enable your device camera permission", string.Empty);
            }
        }
    }
}
