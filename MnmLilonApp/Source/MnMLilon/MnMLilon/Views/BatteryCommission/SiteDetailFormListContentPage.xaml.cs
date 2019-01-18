using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using MnMLilon.ViewModels.BatteryCommission;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class SiteDetailFormListContentPage : ContentPage
    {
        private SiteDetailFormListContentPageViewModel _viewModel;
        public SiteDetailFormListContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            siteDetailInfo.Clicked += SiteDetailInfo_ClickedAsync;

            siteDetailFormListView.ItemSelected += SiteDetailFormListView_ItemSelected;

            _viewModel = (SiteDetailFormListContentPageViewModel)this.BindingContext;
        }

        private void SiteDetailFormListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.NavigateToSiteDetailFormAsync((Category)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        private async void SiteDetailInfo_ClickedAsync(object sender, System.EventArgs e)
        {
            var page = new SiteDetailPopUpPage(BatteryCommissioningSession.Current.CommissionTransactionId);

            await Navigation.PushPopupAsync(page);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
