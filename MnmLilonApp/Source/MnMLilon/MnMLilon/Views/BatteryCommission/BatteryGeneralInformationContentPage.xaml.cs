using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class BatteryGeneralInformationContentPage : ContentPage
    {
        public BatteryGeneralInformationContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            batteryModelNo.Completed += (sender, e) =>
            {
                batterySoftwareVersion.Focus();
            };
        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
