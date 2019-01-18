using MnMLilon.Service.Model;
using MnMLilon.ViewModels.TroubleTicket;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class MaterialsDetailPage : ContentPage
    {
        private MaterialsDetailPageViewModel _viewModel;
        public MaterialsDetailPage()
        {
            InitializeComponent();

            _viewModel = (MaterialsDetailPageViewModel)this.BindingContext;

            materialListView.ItemSelected += MaterialListView_ItemSelected;
        }

        private void MaterialListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.RemoveMaterial((CustomTroubleTicketMaterialModel)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
