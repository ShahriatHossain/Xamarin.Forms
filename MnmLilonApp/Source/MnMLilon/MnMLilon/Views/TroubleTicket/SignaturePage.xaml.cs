using MnMLilon.ViewModels.TroubleTicket;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class SignaturePage : ContentPage
    {
        public SignaturePage()
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
