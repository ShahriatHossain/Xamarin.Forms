using MnMLilon.Service.Configuration;
using MnMLilon.Service.Model;
using MnMLilon.ViewModels.TroubleTicket;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class TroubleTicketTabListContentPage : ContentPage
    {
        private TroubleTicketTabListContentPageViewModel _viewModel;
        public TroubleTicketTabListContentPage()
        {
            InitializeComponent();

            _viewModel = (TroubleTicketTabListContentPageViewModel)this.BindingContext;

            NavigationPage.SetHasBackButton(this, false);

            siteDetailInfo.Clicked += SiteDetailInfo_ClickedAsync;

            tabListView.ItemSelected += TabListView_ItemSelected;
        }

        private void TabListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.NavigateToTabDetailAsync((Category)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        private async void SiteDetailInfo_ClickedAsync(object sender, System.EventArgs e)
        {
            var page = new SiteDetailPopUpPage();

            await Navigation.PushPopupAsync(page);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
