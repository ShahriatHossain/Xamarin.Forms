using MnMFiber.Persistence.Sessions;
using MnMFiber.ViewModels.HotoFiber;
using SignaturePad.Forms;
using System.IO;
using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class SignatureDetailPage : ContentPage
    {
        private SignatureDetailPageViewModel viewModel;
        public SignatureDetailPage()
        {
            InitializeComponent();

            viewModel = (SignatureDetailPageViewModel)this.BindingContext;

            FocusFieldsAfterCompletion();
        }

        private async void saveSign_ActivatedAsync(object sender, System.EventArgs e)
        {
            switch (HotoFiberSession.Current.SelectedSignatureId)
            {
                case "1":
                    viewModel.Signature = (MemoryStream)await padView1.GetImageStreamAsync(SignatureImageFormat.Png);
                    break;

                case "2":
                    viewModel.Signature = (MemoryStream)await padView2.GetImageStreamAsync(SignatureImageFormat.Png);
                    break;

                case "3":
                    viewModel.Signature = (MemoryStream)await padView3.GetImageStreamAsync(SignatureImageFormat.Png);
                    break;
            }

            viewModel.SaveAsync();
        }

        private void FocusFieldsAfterCompletion()
        {
            customerRepresentativeName.Completed += (sender, e) =>
            {
                customerRepresentativeMobileNo.Focus();
            };

            thirdPartyRepresentativeName.Completed += (sender, e) =>
            {
                thirdPartyRepresentativeMobileNo.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
