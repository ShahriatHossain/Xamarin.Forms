using MnMFiber.Core.Models;
using MnMFiber.ViewModels.HotoFiber;
using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class TypeOfSpotsPage : ContentPage
    {
        private TypeOfSpotsPageViewModel _viewModel;
        public TypeOfSpotsPage()
        {
            InitializeComponent();

            _viewModel = (TypeOfSpotsPageViewModel)this.BindingContext;

            typeOfSpots.SelectedIndexChanged += TypeOfSpots_SelectedIndexChanged;

            fiberPlacedOn.SelectedIndexChanged += FiberPlacedOn_SelectedIndexChanged;

            FocusFieldsAfterCompletion();
        }

        private void FiberPlacedOn_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var fiberplacedObj = (CustomCategory)fiberPlacedOn.SelectedItem;
            _viewModel.FiberPlaced = fiberplacedObj;
        }

        private void TypeOfSpots_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var spotTypeObj = (CustomCategory)typeOfSpots.SelectedItem;
            _viewModel.TypeOfSpot = spotTypeObj;
            _viewModel.ShowFieldsRelatedToSpotType(spotTypeObj);
        }

        private void FocusFieldsAfterCompletion()
        {
            towerId.Completed += (sender, e) =>
            {
                towerName.Focus();
            };

            towerName.Completed += (sender, e) =>
            {
                towerAddress.Focus();
            };

            landmark.Completed += (sender, e) =>
            {
                comments.Focus();
            };

            comments.Completed += (sender, e) =>
            {
                numberOfVisibleMH.Focus();
            };

            numberOfVisibleMH.Completed += (sender, e) =>
            {
                numberOfNonVisibleMH.Focus();
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
