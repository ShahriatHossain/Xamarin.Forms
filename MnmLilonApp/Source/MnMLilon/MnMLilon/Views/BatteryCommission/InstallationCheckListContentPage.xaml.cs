using MnMLilon.Service.Model;
using MnMLilon.ViewModels.BatteryCommission;
using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class InstallationCheckListContentPage : ContentPage
    {
        private InstallationCheckListContentPageViewModel _viewModel;
        public InstallationCheckListContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            installationCheckListView.ItemSelected += InstallationCheckListView_ItemSelected;

            _viewModel = (InstallationCheckListContentPageViewModel)this.BindingContext;
        }

        private void InstallationCheckListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.NavigateToInstallationCheckFormAsync((Category)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
