using MnMFiber.Core.Models;
using MnMFiber.ViewModels.HotoFiber;
using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class FiberDetailsPage : ContentPage
    {
        private FiberDetailsPageViewModel _viewModel;
        public FiberDetailsPage()
        {
            InitializeComponent();

            _viewModel = (FiberDetailsPageViewModel)this.BindingContext;

            fiberConstructionType.SelectedIndexChanged += FiberConstructionType_SelectedIndexChanged;

            FocusFieldsAfterCompletion();
        }

        private void FiberConstructionType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var constructionTypeObj = (CustomCategory)fiberConstructionType.SelectedItem;
            _viewModel.FiberConstructionType = constructionTypeObj;
        }

        private void FocusFieldsAfterCompletion()
        {
            noOfFiberCables.Completed += (sender, e) =>
            {
                fiberType.Focus();
            };

            loopLength.Completed += (sender, e) =>
            {
                noOfExtraFibers.Focus();
            };

            noOfExtraFibers.Completed += (sender, e) =>
            {
                noOfDucts.Focus();
            };

            noOfDucts.Completed += (sender, e) =>
            {
                ductColor.Focus();
            };

            ductColor.Completed += (sender, e) =>
            {
                noOfSpareDucts.Focus();
            };

            noOfSpareDucts.Completed += (sender, e) =>
            {
                spareDuctColor.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
