using System.Linq;
using Xamarin.Forms;

namespace MnMFiber.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
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
