using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class ElectricalInstallationContentPage : ContentPage
    {
        public ElectricalInstallationContentPage()
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
