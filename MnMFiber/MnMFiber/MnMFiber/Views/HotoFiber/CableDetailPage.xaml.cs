using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class CableDetailPage : ContentPage
    {
        public CableDetailPage()
        {
            InitializeComponent();

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            cableInLowDepthLength.Completed += (sender, e) =>
            {
                cableInPrivatePropertyLength.Focus();
            };

            cableInPrivatePropertyLength.Completed += (sender, e) =>
            {
                cableInForestAreaLength.Focus();
            };

            cableInForestAreaLength.Completed += (sender, e) =>
            {
                cableInUnderHighwayExpansionLength.Focus();
            };

            cableInUnderHighwayExpansionLength.Completed += (sender, e) =>
            {
                cableInInsideHighwayExpansionLength.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
