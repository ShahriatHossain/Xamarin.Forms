using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class SiteDetailsPage : ContentPage
    {
        public SiteDetailsPage()
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
