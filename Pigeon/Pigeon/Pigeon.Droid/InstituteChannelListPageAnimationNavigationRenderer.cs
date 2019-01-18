using Android.Content.Res;
using Pigeon.Droid;
using Pigeon.ViewModels;
using Pigeon.Views;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using AToolbar = Android.Support.V7.Widget.Toolbar;


[assembly: ExportRenderer(typeof(NavigationPage), typeof(InstituteChannelListPageAnimationNavigationRenderer))]
namespace Pigeon.Droid
{
    public class InstituteChannelListPageAnimationNavigationRenderer : NavigationPageRenderer, Android.Views.View.IOnClickListener
    {
        protected override void SetupPageTransition(Android.Support.V4.App.FragmentTransaction transaction, bool isPush)
        {
            if (isPush)
                transaction.SetCustomAnimations(Resource.Animation.abc_slide_in_top, 0, 0, Resource.Animation.abc_slide_out_top);
            else
                transaction.SetCustomAnimations(Resource.Animation.abc_slide_in_top, 0, 0, Resource.Animation.abc_slide_out_top);
        }

        #region nav toolbar

        private static readonly FieldInfo ToolbarFieldInfo;

        private bool _disposed;
        private AToolbar _toolbar;

        static InstituteChannelListPageAnimationNavigationRenderer()
        {
            // get _toolbar private field info
            ToolbarFieldInfo = typeof(NavigationPageRenderer).GetField("_toolbar",
                    BindingFlags.NonPublic | BindingFlags.Instance)
                ;
        }

        public void OnClick(Android.Views.View v)
        {
            // Call the NavigationPage which will trigger the default behavior
            // The default behavior is to navigate back if the Page derived classes return true from OnBackButtonPressed override            
            var curPage = Element.CurrentPage as InstituteChannelList;
            if (curPage == null)
            {
                Element.PopAsync();
            }
            else
            {
                if (curPage.NeedOverrideSoftBackButton)
                    curPage.OnSoftBackButtonPressed();
                else Element.PopAsync();
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            UpdateToolbarInstance();
        }

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            UpdateToolbarInstance();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                RemoveToolbarInstance();
            }

            base.Dispose(disposing);
        }

        private void UpdateToolbarInstance()
        {
            RemoveToolbarInstance();
            GetToolbarInstance();
        }

        private void GetToolbarInstance()
        {
            try
            {
                //sai o cho nay nay
                //how to get toolbar navigation page
                _toolbar = (AToolbar)ToolbarFieldInfo.GetValue(this);
                _toolbar.SetNavigationOnClickListener(this);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"Can'tbar with error: {exception.Message}");
            }
        }

        private void RemoveToolbarInstance()
        {
            if (_toolbar == null) return;
            _toolbar.SetNavigationOnClickListener(null);
            _toolbar = null;
        }

        #endregion
    }

}