using Rg.Plugins.Popup.Pages;

namespace MnMLilon.Views
{
    public partial class LoaderPopUpPage : PopupPage
    {
        public LoaderPopUpPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
    
}
