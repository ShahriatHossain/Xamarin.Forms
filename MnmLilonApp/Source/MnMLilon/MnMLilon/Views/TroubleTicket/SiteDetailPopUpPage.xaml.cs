using MnMLilon.Service.Configuration;
using MnMLilon.Service.Implementation;
using MnMLilon.Service.Interface;
using MnMLilon.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class SiteDetailPopUpPage : PopupPage
    {
         public SiteDetailPopUpPage()
        {
            InitializeComponent();

            InitiateSiteDetailAsync();
        }

        private void InitiateSiteDetailAsync()
        {
            try
            {
                var siteDetail = TroubleTicketSession.Current.TroubleTicket.SiteDetails;

                siteID.Text = siteDetail.SiteID;
                siteName.Text = siteDetail.SiteName;
                customer.Text = siteDetail.CustomerName;
                circle.Text = siteDetail.CircleName;
                cluster.Text = siteDetail.ClusterName;
            }
            catch(Exception ex)
            {

            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
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
