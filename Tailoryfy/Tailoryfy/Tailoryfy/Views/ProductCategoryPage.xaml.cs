using Tailoryfy.Core.Models;
using Tailoryfy.ViewModels;
using Xamarin.Forms;

namespace Tailoryfy.Views
{
    public partial class ProductCategoryPage : ContentPage
    {
        private readonly ProductCategoryPageViewModel _viewModel;
        public ProductCategoryPage()
        {
            InitializeComponent();

            _viewModel = (ProductCategoryPageViewModel)this.BindingContext;

            productCategoryListView.ItemSelected += ProductCategoryListView_ItemSelected;
        }

        private void ProductCategoryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.GoToDetails((ProductCategory)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
