using Pigeon.Services.Implementation;
using Pigeon.Services.Model;
using Pigeon.ViewModels;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class EditInstitutePage : ContentPage
    {
        private EditInstitutePageViewModel _viewModel;
        
        public EditInstitutePage()
        {
            InitializeComponent();
            _viewModel = (EditInstitutePageViewModel)this.BindingContext;
        }
    }
}
