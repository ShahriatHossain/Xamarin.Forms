using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class ChannelSearchPage : ContentPage
    {
        public ChannelSearchPage()
        {
            InitializeComponent();

            ChannelsView.ItemSelected += ItemSelected;
        }

        void ItemSelected(object sender, System.EventArgs args)
        {
            if (((ListView)sender).SelectedItem == null)
                return;
            //Do stuff here with the SelectedItem ...
            ((ListView)sender).SelectedItem = null;
        }
    }
}
