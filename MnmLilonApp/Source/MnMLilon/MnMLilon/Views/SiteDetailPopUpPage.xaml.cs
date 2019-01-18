using MnMLilon.Service.Implementation;
using MnMLilon.Service.Interface;
using MnMLilon.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MnMLilon.Views
{
    public partial class SiteDetailPopUpPage : PopupPage
    {
         public SiteDetailPopUpPage(int transactionId)
        {
            InitializeComponent();

            InitiateSiteDetailAsync(transactionId);
        }

        private async void InitiateSiteDetailAsync(int transactionId)
        {
            try
            {
                var batteryCommissionService = new BatteryCommissionService();
                var siteDetail = await batteryCommissionService.GetSiteDetails(transactionId);

                siteID.Text = siteDetail.SiteID;
                siteName.Text = siteDetail.SiteName;
                customer.Text = siteDetail.Customer;
                circle.Text = siteDetail.Circle;
                cluster.Text = siteDetail.Cluster;
                createdAt.Text = siteDetail.CreatedAtString;
            }
            catch(Exception ex)
            {

            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }
    }
}
