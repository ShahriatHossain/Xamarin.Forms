using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class FunctionalTestOnBatteryContentPage : ContentPage
    {
        public FunctionalTestOnBatteryContentPage()
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
