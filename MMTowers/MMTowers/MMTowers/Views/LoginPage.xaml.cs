using System.Linq;
using Xamarin.Forms;

namespace MMTowers.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
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
