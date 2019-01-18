using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class FunctionalTestOnBatteryPage : ContentPage
    {
        public FunctionalTestOnBatteryPage()
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
