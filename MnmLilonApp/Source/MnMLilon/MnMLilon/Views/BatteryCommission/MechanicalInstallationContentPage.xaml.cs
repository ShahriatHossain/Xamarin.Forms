using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class MechanicalInstallationContentPage : ContentPage
    {
        public MechanicalInstallationContentPage()
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
