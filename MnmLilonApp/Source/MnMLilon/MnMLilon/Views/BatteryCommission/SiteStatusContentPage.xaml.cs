using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class SiteStatusContentPage : ContentPage
    {
        public SiteStatusContentPage()
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
