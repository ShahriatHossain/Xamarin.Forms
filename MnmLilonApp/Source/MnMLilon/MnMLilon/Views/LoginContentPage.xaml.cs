using MnMLilon.Helper;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System.Linq;
using Xamarin.Forms;

namespace MnMLilon.Views
{
    public partial class LoginContentPage : ContentPage
    {
        public LoginContentPage()
        {
            InitializeComponent();

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            userName.Completed += (sender, e) =>
            {
                password.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            ClearNavigationStack();
            return false;
        }
        private void ClearNavigationStack()
        {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page != this)
                    Navigation.RemovePage(page);
            }
        }
    }
}
