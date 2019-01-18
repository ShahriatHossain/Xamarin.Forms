using MnMLilon.ViewModels.TroubleTicket;
using SignaturePad.Forms;
using System.IO;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class SignPage : ContentPage
    {
        private SignPageViewModel _viewModel;
        public SignPage()
        {
            InitializeComponent();

            _viewModel = (SignPageViewModel)this.BindingContext;

            NavigationPage.SetHasBackButton(this, false);

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
            _viewModel.Signature = (MemoryStream)await padView.GetImageStreamAsync(SignatureImageFormat.Png);

            _viewModel.SaveAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
