using System;
using Pigeon.ViewModels;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class DisplayLargeImagePage : ContentPage
    {
        public DisplayLargeImagePage()
        {
            InitializeComponent();
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
        }
    }
}
