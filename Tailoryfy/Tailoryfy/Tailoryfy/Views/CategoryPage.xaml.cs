using Tailoryfy.Core.Models;
using Tailoryfy.ViewModels;
using Xamarin.Forms;

namespace Tailoryfy.Views
{
    public partial class CategoryPage : ContentPage
    {
        private readonly CategoryPageViewModel _viewModel;
        public CategoryPage()
        {
            InitializeComponent();
            _viewModel = (CategoryPageViewModel)this.BindingContext;
            categoryListView.ItemSelected += CategoryListView_ItemSelected;
        }

        private void CategoryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.GoToDetails((CustomCategory)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
