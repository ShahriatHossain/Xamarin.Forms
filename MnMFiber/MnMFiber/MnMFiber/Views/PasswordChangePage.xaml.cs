using Xamarin.Forms;

namespace MnMFiber.Views
{
    public partial class PasswordChangePage : ContentPage
    {
        public PasswordChangePage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
