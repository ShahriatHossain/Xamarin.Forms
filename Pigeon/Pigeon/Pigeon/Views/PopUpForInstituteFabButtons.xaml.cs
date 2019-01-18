using Pigeon.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class PopUpForInstituteFabButtons : PopupPage
    {
        private PopUpForInstituteFabButtonsViewModel _viewModel;

        public PopUpForInstituteFabButtons()
        {
            InitializeComponent();

            _viewModel = (PopUpForInstituteFabButtonsViewModel)this.BindingContext;
        }


        private void DiscoverInstituteFab_Clicked(object sender, EventArgs e)
        {
            this.ClosePopup();
            _viewModel.DiscoverInstitute();
        }

        private void CreateInstituteFab_Clicked(object sender, EventArgs e)
        {
            this.ClosePopup();
            _viewModel.CreateInstitute();
        }

        private void CloseInstituteFab_Clicked(object sender, EventArgs e)
        {
            this.ClosePopup();
        }

        private void ClosePopup()
        {
            PopupNavigation.Instance.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            this.ClosePopup();
            return true;
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }
    }
}
