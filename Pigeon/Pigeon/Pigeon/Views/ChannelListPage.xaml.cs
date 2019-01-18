using Pigeon.Services.Model;
using Pigeon.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class ChannelListPage : ContentPage
    {
        private ChannelListPageViewModel _viewModel;

        public ChannelListPage()
        {
            InitializeComponent();
            _viewModel = (ChannelListPageViewModel)this.BindingContext;

            myChannelListView.ItemSelected += ChannelListView_ItemSelected;
            subscribedChannelListView.ItemSelected += SubscribedChannelListView_ItemSelected;
        }

        private void SubscribedChannelListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.ChannelDetail((ChannelApi)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        private void ChannelListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            _viewModel.ChannelDetail((ChannelApi)e.SelectedItem);
            ((ListView)sender).SelectedItem = null;
        }

        private async void AddChannelFab_ClickedAsync(object sender, EventArgs e)
        {
            var page = new PopUpForInstituteFabButtons();
            await Navigation.PushPopupAsync(page);
        }
    }
}
