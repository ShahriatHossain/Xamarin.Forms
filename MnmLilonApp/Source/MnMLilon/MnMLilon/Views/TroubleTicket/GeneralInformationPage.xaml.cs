using MnMLilon.Service.Model;
using MnMLilon.ViewModels.TroubleTicket;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class GeneralInformationPage : ContentPage
    {
        private GeneralInformationPageViewModel _viewModel;
        public GeneralInformationPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (GeneralInformationPageViewModel)this.BindingContext;

            problemRelatedList.SelectedIndexChanged += ProblemRelatedList_SelectedIndexChanged;

            FocusFieldsAfterCompletion();
        }

        private void ProblemRelatedList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            _viewModel.ProblemRelated = (Category)problemRelatedList.SelectedItem;
        }

        private void FocusFieldsAfterCompletion()
        {
            noOfModulesAtSite.Completed += (sender, e) =>
            {
                noOfJunctionBoxAtSite.Focus();
            };

            noOfJunctionBoxAtSite.Completed += (sender, e) =>
            {
                siteObservation.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
