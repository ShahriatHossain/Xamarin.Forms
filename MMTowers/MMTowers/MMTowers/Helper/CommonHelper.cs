using MMTowers.Views;
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

namespace MMTowers.Helper
{
    public class CommonHelper
    {
        public static async void DisplayAlertMessage(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
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

        public static Double DistanceInMetres(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 = ((double)((int)(lat1 * 100.0))) / 100.0;
            lon1 = ((double)((int)(lon1 * 100.0))) / 100.0;

            lat2 = ((double)((int)(lat2 * 100.0))) / 100.0;
            lon2 = ((double)((int)(lon2 * 100.0))) / 100.0;

            if (lat1 == lat2 && lon1 == lon2)
                return 0.0;

            var theta = lon1 - lon2;

            var distance = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
                           Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
                           Math.Cos(deg2rad(theta));

            distance = Math.Acos(distance);
            if (double.IsNaN(distance))
                return 0.0;

            distance = rad2deg(distance);
            distance = distance * 60.0 * 1.1515 * 1609.344;

            return (distance);
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        public static async Task ShowLoaderAsync()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopUpPage());
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
