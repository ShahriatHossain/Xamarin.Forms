using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.ViewModels.HotoFiber;
using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class ViewSpotsPage : ContentPage
    {
        private ViewSpotsPageViewModel _viewModel;

        public ViewSpotsPage()
        {
            InitializeComponent();

            _viewModel = (ViewSpotsPageViewModel)this.BindingContext;

            spotListView.ItemSelected += SpotListView_ItemSelected;
        }

        private void SpotListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.GoToSpotDetailPage((TicketSpotDto)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
