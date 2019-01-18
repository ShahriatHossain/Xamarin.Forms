using MnMLilon.ViewModels.BatteryCommission;
using SignaturePad.Forms;
using System.IO;
using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class SignatureContentPage : ContentPage
    {
        private SignatureContentPageViewModel _viewModel;
        public SignatureContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (SignatureContentPageViewModel)this.BindingContext;

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            customerContactPersonName.Completed += (sender, e) =>
            {
                customerContactPersonMobile.Focus();
            };

            customerContactPersonMobile.Completed += (sender, e) =>
            {
                customerContactPersonDesignation.Focus();
            };
        }

        private async void saveSign_ActivatedAsync(object sender, System.EventArgs e)
        {
            _viewModel.Signature = (MemoryStream) await padView.GetImageStreamAsync(SignatureImageFormat.Png);

            _viewModel.SaveAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
