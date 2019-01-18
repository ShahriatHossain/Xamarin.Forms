using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class PhotosAndSignatureContentPage : ContentPage
    {
        public PhotosAndSignatureContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
