using Rg.Plugins.Popup.Pages;

namespace MMTowers.Views
{
    public partial class LoadingPopUpPage : PopupPage
    {
        public LoadingPopUpPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
