using MnMLilon.ViewModels.TroubleTicket;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class BatteryDetailsPage : ContentPage
    {
        private BatteryDetailsPageViewModel _viewModel;
        public BatteryDetailsPage()
        {
            InitializeComponent();

            _viewModel = (BatteryDetailsPageViewModel)this.BindingContext;

            NavigationPage.SetHasBackButton(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void OnTapGestureRecognizerTapped(object sender, System.EventArgs e)
        {
            Image img = (Image)sender;
            var gesture = (TapGestureRecognizer)img.GestureRecognizers[0];
            var batteryId = System.Convert.ToInt32(gesture.CommandParameter);

            var page = new EditBatteryPopupPage(batteryId);
            await Navigation.PushPopupAsync(page);
        }
    }
}
