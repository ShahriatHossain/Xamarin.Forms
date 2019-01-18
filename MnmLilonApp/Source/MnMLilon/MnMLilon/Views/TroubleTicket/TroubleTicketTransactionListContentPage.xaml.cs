using MnMLilon.Service.Model;
using MnMLilon.ViewModels.TroubleTicket;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class TroubleTicketTransactionListContentPage : ContentPage
    {
        private TroubleTicketTransactionListContentPageViewModel _viewModel;
        public TroubleTicketTransactionListContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (TroubleTicketTransactionListContentPageViewModel)this.BindingContext;

            pendingTicketListView.ItemSelected += PendingTicketListView_ItemSelected;
        }

        private void PendingTicketListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.NavigateToTabListAsync((PendingTroubleTicketModel)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
