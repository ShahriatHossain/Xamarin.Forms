using MnMLilon.Service.Configuration;
using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class BatteryBankDataInformationContentPage : ContentPage
    {
        public BatteryBankDataInformationContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            batteryName.Completed += (sender, e) =>
            {
                packVoltage.Focus();
            };

            packVoltage.Completed += (sender, e) =>
            {
                socPercent.Focus();
            };

            socPercent.Completed += (sender, e) =>
            {
                sohPercent.Focus();
            };

            sohPercent.Completed += (sender, e) =>
            {
                cellVoltageMin.Focus();
            };

            cellVoltageMin.Completed += (sender, e) =>
            {
                cellVoltageMax.Focus();
            };

            cellVoltageMax.Completed += (sender, e) =>
            {
                mosfetTemp.Focus();
            };

            mosfetTemp.Completed += (sender, e) =>
            {
                cellTempMin.Focus();
            };

            cellTempMin.Completed += (sender, e) =>
            {
                cellTempMax.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
