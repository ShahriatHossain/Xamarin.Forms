using MnMLilon.Service.Interface;
using MnMLilon.Views;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MnMLilon.Helper
{
    public class CommonHelper
    {
        public static async void DisplayAlertMessage(string title, string message)
        {
            try
            {
                if (Application.Current.MainPage != null)
                    await Application.Current.MainPage.DisplayAlert(title, message, "OK");
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task<bool> AlertMessageWithCancelOrOkAsync(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "Ok", "Cancel");
        }

        public static bool HasInternetConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public static async Task<bool> IsUserInDesiredLocation(double Latitude, double Longitude)
        {
            var meters = await CommonHelper.CheckCurrentGeoLocationAsync(Latitude, Longitude);
            if (meters <= 100)
            {
                return true;
            }
            return false;
        }

        public static async Task<bool> IsLocationEnabledFromDevice()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                if (position == null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private static async Task<double> CheckCurrentGeoLocationAsync(double Latitude, double Longitude)
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                if (position == null)
                {
                    return 0.0;
                }

                var inMeters = DistanceInExpectedMeters(position.Latitude, position.Longitude, Latitude, Longitude);
                return inMeters;
            }
            catch (Exception ex)
            {
                DisplayAlertMessage("", "Please enable your device location.");
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }

            return 0.0;
        }

        public static Double DistanceInExpectedMeters(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 = ((double)((int)(lat1 * 1000.0))) / 1000.0;
            lon1 = ((double)((int)(lon1 * 1000.0))) / 1000.0;

            lat2 = ((double)((int)(lat2 * 1000.0))) / 1000.0;
            lon2 = ((double)((int)(lon2 * 1000.0))) / 1000.0;

            double lat1Minus = lat1 - 0.005;
            double lat1Plus = lat1 + 0.005;

            double lon1Minus = lon1 - 0.005;
            double lon1Plus = lon1 + 0.005;

            if ((lat2 >= lat1Minus && lat2 <= lat1Plus) && (lon2 >= lon1Minus && lon2 <= lon1Plus))
            {
                return 0.0;
            }

            return 100000;
        }

        public static async Task<Position> GetDeviceCurrentLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

                if (position != null)
                {
                    return position;
                }
            }
            catch (Exception ex)
            {
                DisplayAlertMessage("", "Please enable your device location.");
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }

            return new Position();
        }

        public static void SubscribeTopicForNotification(string topicId)
        {
            var notificationService = DependencyService.Get<IPushNotificationService>();
            notificationService.SubscribeTopicForNotification(topicId);
        }

        public static void UnSubscribeTopicForNotification(string topicId)
        {
            var notificationService = DependencyService.Get<IPushNotificationService>();
            notificationService.UnSubscribeTopicForNotification(topicId);
        }

        public static decimal GetTextboxValue(string textboxText)
        {
            return Convert.ToDecimal(string.IsNullOrEmpty(textboxText) ? "0" : textboxText);
        }

        public static string SetTextboxValue(string textboxText)
        {
            return (textboxText == "0") ? string.Empty : textboxText;
        }

        public static async Task ShowLoaderAsync()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoaderPopUpPage());
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task HideLoaderAsync()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public byte[] GetFileDataArray(string filePath)
        {
            return DependencyService.Get<ILocalFileManageService>().GetFileDataArray(filePath);
        }

        public static async void PromptToStoragePermissionEnabled()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                }
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

            await IsLocationSupportedAsync();
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
                        DisplayAlertMessage("", "Please enable your device storage permission");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                        status = results[Permission.Storage];
                }


            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Please enable your device storage permission");
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
                        DisplayAlertMessage("", "Please enable your device photos permission");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Photos))
                        status = results[Permission.Photos];
                }


            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Please enable your device photos permission");
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
                        DisplayAlertMessage("", "Please enable your device location permission");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }


            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Please enable your device location permission");
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
                        DisplayAlertMessage("", "Please enable your device location permission");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }


            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Please enable your device location permission");
            }
        }
    }
}
