using Pigeon.Services.Configuration;
using Pigeon.Services.Implementation;
using Pigeon.Services.Model;
using Pigeon.ViewModels;
using System;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class ChannelDetailViewer : ContentPage
    {
        private ChannelDetailViewerViewModel _viewModel;

        public ChannelDetailViewer()
        {
            InitializeComponent();
            _viewModel = (ChannelDetailViewerViewModel)this.BindingContext;
            lv.ItemAppearing += Lv_ItemAppearing;

            lv.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row
                if (e.Item == null) return;
                // do something with e.SelectedItem
                ((ListView)sender).SelectedItem = null; // de-select the row
            };

        }

        private async void SetAddFabButtonAndToolbarItemsVisibility()
        {
            var selectedChannel = InstituteSession.Current.SelectedChannel;

            ChannelApi channel = await new ChannelService().GetChannelDetailAsync(selectedChannel, _viewModel.CStatus);
            if (channel != null)
            {
                securePin.IsDestructive = !channel.OwnerFlag;
                subscribers.IsDestructive = !channel.OwnerFlag;
            }

        }

        private void Lv_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if (_viewModel.HasNewMessage)
                {
                    _viewModel.HasNewMessage = false;
                    lv.ScrollTo(_viewModel.CurrentMessage, ScrollToPosition.MakeVisible, true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AddChannelFab_Clicked(object sender, EventArgs e)
        {
            _viewModel.AddNewNotice();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.SetAddFabButtonAndToolbarItemsVisibility();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            InstituteSession.Current.SetSelectedChannel(0);
        }


        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as ChannelDetailViewerViewModel;
            var result = bindingContext?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
            return result;
        }

        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as ChannelDetailViewerViewModel;
            bindingContext?.OnSoftBackButtonPressed();
        }

        public bool NeedOverrideSoftBackButton { get; set; } = true;
    }
}
