using MnMLilon.Service.Model;
using MnMLilon.ViewModels.BatteryCommission;
using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class CommissionTransactionListContentPage : ContentPage
    {
        private CommissionTransactionListContentPageViewModel _viewModel;
        public CommissionTransactionListContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (CommissionTransactionListContentPageViewModel)this.BindingContext;

            commissionTransactionListView.ItemSelected += CommissionTransactionListView_ItemSelected;
        }

        private void CommissionTransactionListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.NavigateToSiteDetailFormListAsync((TechnicianTransaction)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
