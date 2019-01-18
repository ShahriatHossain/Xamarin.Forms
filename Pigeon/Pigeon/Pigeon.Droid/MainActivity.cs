using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Util;
using ButtonCircle.FormsPlugin.Droid;
using Firebase.Iid;
using Firebase.Messaging;
using Pigeon.Services.Interface;
using Pigeon.Views;
using System;
using Xamarin.Forms;

namespace Pigeon.Droid
{
    [Activity(Label = "Pigeon", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            // for CircleButton
            ButtonCircleRenderer.Init();


            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();


            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    //Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }

            IsPlayServicesAvailable();

            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (width - 0.5f) / density;
            App.ScreenHeight = (height - 0.5f) / density;

            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustResize);
            AndroidBug5497WorkaroundForXamarinAndroid.assistActivity(this);

            LoadApplication(new App());
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode)) { }
                else
                {
                    Finish();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (data.HasExtra("customParam"))
            {
                var customParam = data.GetStringExtra("channelId");
                if (!String.IsNullOrEmpty(customParam))
                {
                    data.RemoveExtra("customParam");
                    // Do your navigation or other functions here
                }
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            if (intent != null)
            {
                var channelId = intent.GetStringExtra("channelId");

                if (!string.IsNullOrEmpty(channelId))
                    DependencyService.Get<ICustomNavigationService>();
            }
        }
    }
}

