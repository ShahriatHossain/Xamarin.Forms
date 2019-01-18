using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Pigeon.Droid;
using Pigeon.Services.Interface;
using Pigeon.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(CustomNavigationService))]
namespace Pigeon.Droid
{
    public class CustomNavigationService : ICustomNavigationService
    {
        public void NavigateToRootPage()
        {
            App.Current.MainPage = new NavigationPage(new InstituteTabbedPage());
        }
    }
}