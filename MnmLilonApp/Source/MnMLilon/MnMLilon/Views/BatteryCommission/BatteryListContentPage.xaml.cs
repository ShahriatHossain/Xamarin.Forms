using MnMLilon.Service.Model;
using MnMLilon.ViewModels.BatteryCommission;
using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class BatteryListContentPage : ContentPage
    {
        private BatteryListContentPageViewModel _viewModel;
        public BatteryListContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (BatteryListContentPageViewModel)this.BindingContext;

            batteryListView.ItemSelected += BatteryListView_ItemSelected;
        }

        private void BatteryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.NavigateToBatteryBankDataInformationContentPageAsync((Category)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
