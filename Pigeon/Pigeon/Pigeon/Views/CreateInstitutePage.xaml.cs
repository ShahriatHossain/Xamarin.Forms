using Pigeon.Services.Implementation;
using Pigeon.Services.Model;
using Pigeon.ViewModels;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class CreateInstitutePage : ContentPage
    {
        private CreateInstitutePageViewModel _viewModel;
        public CreateInstitutePage()
        {
            InitializeComponent();

            _viewModel = (CreateInstitutePageViewModel)this.BindingContext;

            instituteCategory.SelectedIndexChanged += InstituteCategory_SelectedIndexChanged;

            instituteType.SelectedIndexChanged += InstituteType_SelectedIndexChanged;

            SetDefaultPickerValueAsync();
        }

        private void InstituteType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var type = (InstituteType)instituteType.SelectedItem;
            _viewModel.InstituteTypeId = type.Id;
        }

        private void InstituteCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var category = (InstituteCategory)instituteCategory.SelectedItem;
            _viewModel.InstituteCategoryId = category.Id;
        }

        private async void SetDefaultPickerValueAsync()
        {
            var instituteCategoryList = await new InstituteCategoryService().GetInstituteCategoriesAsync();
            instituteCategory.ItemsSource = instituteCategoryList;

            var instituteTypeList = await new InstituteTypeService().GetInstituteTypesAsync();
            instituteType.ItemsSource = instituteTypeList;
        }
    }
}
