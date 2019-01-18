using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.ViewModels.HotoFiber;
using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class HotoTicketListPage : ContentPage
    {
        private HotoTicketListPageViewModel _viewModel;
        public HotoTicketListPage()
        {
            InitializeComponent();

            _viewModel = (HotoTicketListPageViewModel)this.BindingContext;

            ticketListView.ItemSelected += TicketListView_ItemSelected;
        }

        private void TicketListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.GoToTicketDetailPage((TicketBasicDto)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
