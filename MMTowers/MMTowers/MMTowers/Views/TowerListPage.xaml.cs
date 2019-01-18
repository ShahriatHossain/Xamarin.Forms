using MMTowers.Service.Model;
using MMTowers.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace MMTowers.Views
{
    public partial class TowerListPage : ContentPage
    {
        public TowerListPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (TowerListPageViewModel)this.BindingContext;
            searchBar.TextChanged += SearchBar_TextChanged;
            towerListView.ItemSelected += TowerListView_ItemSelected;
        }

        private void TowerListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            _viewModel.NavigateToTowerDetailAsync((Tower)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        private TowerListPageViewModel _viewModel;

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            towerListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                towerListView.ItemsSource = _viewModel.TowerList;
            else
                towerListView.ItemsSource = _viewModel.TowerList.Where(i => i.BTSName.ToLower().Contains(e.NewTextValue));

            towerListView.EndRefresh();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.GetAllTowers();
        }
    }
}
