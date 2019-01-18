using MnMFiber.Core.Repositories;
using MnMFiber.Views;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MnMFiber.Common.Helper
{
    public class CommonHelper
    {
        public static bool DoIHaveInternet()
        {
            if (!CrossConnectivity.IsSupported)
                return true;

            //Do this only if you need to and aren't listening to any other events as they will not fire.
            using (var connectivity = CrossConnectivity.Current)
            {
                return connectivity.IsConnected;
            }
        }

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

        public static bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public static bool IsLocationEnabled()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationEnabled;
        }

        public static bool CheckStatusOfLocationAvailabilityAsync()
        {
            if (!IsLocationEnabled())
            {
                //DisplayAlertMessage("", "Please enable your device location.");
                return false;
            }

            return true;
        }

        public static async Task<bool> IsUserInDesiredLocation(double Latitude, double Longitude)
        {
            var meters = await CheckCurrentGeoLocationAsync(Latitude, Longitude);
            if (meters <= 100)
            {
                return true;
            }
            return false;
        }

        private static async Task<double> CheckCurrentGeoLocationAsync(double Latitude, double Longitude)
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
                if (position == null)
                {
                    return 0.0;
                }

                var inMeters = DistanceInExpectedMeters(position.Latitude, position.Longitude, Latitude, Longitude);
                return inMeters;
            }
            catch (Exception ex)
            {
                //DisplayAlertMessage("", "Please enable your device location.");
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

        public static async Task<Position> GetDeviceCurrentLocation()
        {
            try
            {
                if (CheckStatusOfLocationAvailabilityAsync())
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 100;

                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10000));

                    if (position != null)
                    {
                        return position;
                    }
                }
            }
            catch (Exception ex)
            {
                //DisplayAlertMessage("", "Please enable your device location.");
            }

            return new Position();
        }

        public static async void CheckAllPluginsPermissionStatus()
        {
            try
            {
                await CheckPermissionForEachPlugin(Permission.Location, "Location");

                await CheckPermissionForEachPlugin(Permission.Storage, "Storage");
            }
            catch (Exception ex)
            {

                DisplayAlertMessage("", "Can not continue, try again.");
            }
        }

        private static async Task CheckPermissionForEachPlugin(Permission permission, string permissionTitlte)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                {
                    DisplayAlertMessage("", "Gonna need that " + permissionTitlte);
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                //Best practice to always check that the key exists
                if (results.ContainsKey(permission))
                    status = results[permission];
            }

            if (status == PermissionStatus.Granted)
            {

            }
            else if (status != PermissionStatus.Unknown)
            {
                DisplayAlertMessage(permissionTitlte + " Denied", "Can not continue, try again.");
            }
        }

        public static async Task ShowLoaderAsync()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopupPage());
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
    }
}
