using MnMLilon.Service.Model;
using MnMLilon.ViewModels.TroubleTicket;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class PowerPlantOtherDetailsPage : ContentPage
    {
        public PowerPlantOtherDetailsPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            siteComments.Completed += (sender, e) =>
            {
                powerPlantSoftwareVersion.Focus();
            };

            powerPlantSoftwareVersion.Completed += (sender, e) =>
            {
                chargingVoltage.Focus();
            };

            chargingVoltage.Completed += (sender, e) =>
            {
                maxChargingModule.Focus();
            };

            maxChargingModule.Completed += (sender, e) =>
            {
                lvdSettings.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
