using Pigeon.Services.Implementation;
using Pigeon.Services.Model;
using Pigeon.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class ChannelEditPage : ContentPage
    {

        private ChannelEditPageViewModel _viewModel;
        public ChannelEditPage()
        {
            InitializeComponent();
            _viewModel = (ChannelEditPageViewModel)this.BindingContext;
            channelCategory.SelectedIndexChanged += ChannelCategory_SelectedIndexChanged;
        }

        private void ChannelCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var category = (ChannelCategory)channelCategory.SelectedItem;
            _viewModel.ChannelCategoryId = category.Id;
        }

    }
}
