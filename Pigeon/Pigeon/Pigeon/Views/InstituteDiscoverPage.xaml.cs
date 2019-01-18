using Pigeon.Services.Model;
using Pigeon.ViewModels;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class InstituteDiscoverPage : ContentPage
    {
        private InstituteDiscoverPageViewModel _viewModel;
        public InstituteDiscoverPage()
        {
            InitializeComponent();

            _viewModel = (InstituteDiscoverPageViewModel)this.BindingContext;

            instituteListView.ItemSelected += InstituteListView_ItemSelected;
            var searchBar = new SearchBar
            {
                HeightRequest = 50
            };
        }

        private void InstituteListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.InstituteDetail((Institute)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as InstituteDiscoverPageViewModel;
            bindingContext?.OnSoftBackButtonPressed();
        }
        public bool NeedOverrideSoftBackButton { get; set; } = true;
    }
}
