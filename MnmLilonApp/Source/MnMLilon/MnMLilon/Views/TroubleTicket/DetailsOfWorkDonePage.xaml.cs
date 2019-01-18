using MnMLilon.Service.Model;
using MnMLilon.ViewModels.TroubleTicket;
using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class DetailsOfWorkDonePage : ContentPage
    {
        private DetailsOfWorkDonePageViewModel _viewModel;
        public DetailsOfWorkDonePage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (DetailsOfWorkDonePageViewModel)this.BindingContext;

            statusOfComplaints.SelectedIndexChanged += StatusOfComplaints_SelectedIndexChanged;

            materialsDescription.SelectedIndexChanged += MaterialsDescription_SelectedIndexChanged;

            materialType.SelectedIndexChanged += MaterialType_SelectedIndexChanged;

            addMaterial.Clicked += AddMaterial_Clicked;

            FocusFieldsAfterCompletion();
        }

        private void MaterialType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var materialTypeObj = (Category)materialType.SelectedItem;
            _viewModel.MaterialType = materialTypeObj;
        }

        private void FocusFieldsAfterCompletion()
        {
            alarmsNotice.Completed += (sender, e) =>
            {
                serviceEngineerRemarks.Focus();
            };

            serviceEngineerRemarks.Completed += (sender, e) =>
            {
                statusOfComplaints.Focus();
            };

            statusOfComplaints.SelectedIndexChanged += (sender, e) =>
            {
                statusRemarks.Focus();
            };

            statusRemarks.Completed += (sender, e) =>
            {
                serviceEngineerRemarksWithRca.Focus();
            };

            serviceEngineerRemarksWithRca.Completed += (sender, e) =>
            {
                recommendation.Focus();
            };
        }

        private void AddMaterial_Clicked(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(quantity.Text))
                materialsDescription.SelectedIndex = -1;
        }

        private void MaterialsDescription_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var sparePart = (SparePartModel)materialsDescription.SelectedItem;
            _viewModel.SparePart = sparePart;

            partNo.Text = sparePart.PartSerialNo;
            _viewModel.HasTroubleTicketMaterialSelected = true;
        }

        private void StatusOfComplaints_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var statusComplaint = (Category)statusOfComplaints.SelectedItem;
            _viewModel.StatusComplaint = statusComplaint;

            if (statusComplaint.Id == 2)
            {
                statusRemarks.IsVisible = true;
            }
            else
            {
                statusRemarks.IsVisible = false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
