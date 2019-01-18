using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class HotoTicketDetailPage : ContentPage
    {
        public HotoTicketDetailPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
