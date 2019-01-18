using MnMLilon.Service.Model;
using MnMLilon.ViewModels.TroubleTicket;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class EquipmentDetailsPage : ContentPage
    {
        private EquipmentDetailsPageViewModel _viewModel;
        public EquipmentDetailsPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (EquipmentDetailsPageViewModel)this.BindingContext;

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

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
