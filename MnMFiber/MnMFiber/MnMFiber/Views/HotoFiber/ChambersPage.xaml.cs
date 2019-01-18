using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class ChambersPage : ContentPage
    {
        public ChambersPage()
        {
            InitializeComponent();

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            chamberVisibility.Completed += (sender, e) =>
            {
                chamberNonVisibility.Focus();
            };

            chamberNonVisibility.Completed += (sender, e) =>
            {
                trayJointsRouteWiseCount.Focus();
            };

            trayJointsRouteWiseCount.Completed += (sender, e) =>
            {
                buiredJcChamberCount.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
