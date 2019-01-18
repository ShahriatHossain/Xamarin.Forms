using Pigeon.Services.Configuration;
using Pigeon.Services.Implementation;
using Pigeon.Services.Model;
using Pigeon.ViewModels;
using System;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class InstituteChannelList : ContentPage
    {
        private InstituteChannelListViewModel _viewModel;

        public InstituteChannelList()
        {
            InitializeComponent();
            _viewModel = (InstituteChannelListViewModel)this.BindingContext;
            channelListView.ItemSelected += ChannelListView_ItemSelected;
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

        private void AddChannelFab_Clicked(object sender, EventArgs e)
        {
            _viewModel.CreateChannel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetDefaultFieldsValue();
        }

        private async void SetDefaultFieldsValue()
        {
            try
            {
                var instituteId = InstituteSession.Current.SelectedInstitute;

                Institute institute = await new InstituteService().GetInstituteDetailAsync(instituteId);
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            InstituteSession.Current.SetSelectedInstitute(0);
        }

        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as InstituteChannelListViewModel;
            var result = bindingContext?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
            return result;
        }

        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as InstituteChannelListViewModel;
            bindingContext?.OnSoftBackButtonPressed();
        }

        public bool NeedOverrideSoftBackButton { get; set; } = true;
    }
}
