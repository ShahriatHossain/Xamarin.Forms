using Pigeon.Services.Model;
using Pigeon.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class InstituteListPage : ContentPage
    {
        private InstituteListPageViewModel _viewModel;

        public InstituteListPage()
        {
            InitializeComponent();

            try
            {
                _viewModel = (InstituteListPageViewModel)this.BindingContext;

                myInstituteListView.ItemSelected += InstituteListView_ItemSelected;
                subscribedInstituteListView.ItemSelected += SubscribedInstituteListView_ItemSelected;
            }
            catch (Exception ex)
            {

            }
        }

        private async void AddInstituteFab_Clicked(object sender, EventArgs e)
        {
            var page = new PopUpForInstituteFabButtons();
            await Navigation.PushPopupAsync(page);
        }

        private void SubscribedInstituteListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.InstituteDetail((Institute)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
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

    }
}
