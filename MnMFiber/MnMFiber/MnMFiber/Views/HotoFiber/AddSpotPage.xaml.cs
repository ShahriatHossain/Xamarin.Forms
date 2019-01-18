using MnMFiber.Core.Models;
using MnMFiber.ViewModels.HotoFiber;
using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class AddSpotPage : ContentPage
    {
        private AddSpotPageViewModel _viewModel;
        public AddSpotPage()
        {
            InitializeComponent();

            _viewModel = (AddSpotPageViewModel)this.BindingContext;

            categoryListView.ItemSelected += CategoryListView_ItemSelected;
        }

        private void CategoryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.GoToCategoryDetailPage(((CustomCategory)e.SelectedItem).Id);
            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
