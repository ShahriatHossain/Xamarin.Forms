using MnMFiber.Core.Models;
using MnMFiber.ViewModels.PatrollerDailySurveillance;
using Xamarin.Forms;

namespace MnMFiber.Views.PatrollerDailySurveillance
{
    public partial class PatrollerDailySurveillancePage : ContentPage
    {
        private readonly PatrollerDailySurveillancePageViewModel _viewModel;
        public PatrollerDailySurveillancePage()
        {
            InitializeComponent();

            _viewModel = (PatrollerDailySurveillancePageViewModel)this.BindingContext;

            buttonListView.ItemSelected += ButtonListView_ItemSelected;
        }

        private void ButtonListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.GoToDetailPage((CustomCategory)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
