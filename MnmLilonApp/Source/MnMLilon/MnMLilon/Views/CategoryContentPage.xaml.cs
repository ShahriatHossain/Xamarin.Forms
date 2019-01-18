using MnMLilon.Service.Model;
using MnMLilon.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace MnMLilon.Views
{
    public partial class CategoryContentPage : ContentPage
    {
        private CategoryContentPageViewModel _viewModel;
        public CategoryContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (CategoryContentPageViewModel)this.BindingContext;

            categoryListView.ItemSelected += CategoryListView_ItemSelected;
        }

        private void CategoryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.NavigateToCommissionTransactionListAsync((Category)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            ClearNavigationStack();
            return false;
        }
        private void ClearNavigationStack()
        {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page != this)
                    Navigation.RemovePage(page);
            }
        }
    }
}
