using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class UserRegistrationPage : ContentPage
    {
        public UserRegistrationPage()
        {
            InitializeComponent();

            email.Completed += (sender, e) =>
            {
                countrycode.Focus();
            };

            countrycode.Completed += (sender, e) =>
            {
                mobileno.Focus();
            };
        }

    }
}
