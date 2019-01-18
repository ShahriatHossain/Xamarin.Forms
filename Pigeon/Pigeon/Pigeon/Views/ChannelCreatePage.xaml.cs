using Pigeon.Services.Implementation;
using Pigeon.Services.Model;
using Pigeon.ViewModels;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class ChannelCreatePage : ContentPage
    {
        private ChannelCreatePageViewModel _viewModel;
        public ChannelCreatePage()
        {
            InitializeComponent();

            _viewModel = (ChannelCreatePageViewModel)this.BindingContext;

            channelCategory.SelectedIndexChanged += ChannelCategory_SelectedIndexChanged;

            this.SetDefaultPickerValueAsync();
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

        private async void SetDefaultPickerValueAsync()
        {
            var channelCategoryList = await new ChannelCategoryService().GetChannelCategoriesAsync();
            channelCategory.ItemsSource = channelCategoryList;
        }
    }
}
