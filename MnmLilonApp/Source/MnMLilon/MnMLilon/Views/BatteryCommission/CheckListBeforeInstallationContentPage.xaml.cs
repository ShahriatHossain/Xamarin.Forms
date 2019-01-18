using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class CheckListBeforeInstallationContentPage : ContentPage
    {
        public CheckListBeforeInstallationContentPage()
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
