using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class AerialCablePage : ContentPage
    {
        public AerialCablePage()
        {
            InitializeComponent();

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            armoredCableAsAerial.Completed += (sender, e) =>
            {
                structuredAerial.Focus();
            };

            structuredAerial.Completed += (sender, e) =>
            {
                temporaryAerial.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
