using MMTowers.ViewModels;
using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MMTowers.Views
{
    public partial class DieselRefillPage : ContentPage
    {
        public DieselRefillPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
