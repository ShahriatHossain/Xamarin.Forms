using MnMFiber.ViewModels.HotoFiber;
using Xamarin.Forms;

namespace MnMFiber.Views.HotoFiber
{
    public partial class TypeOfCrossingsPage : ContentPage
    {
        private readonly TypeOfCrossingsPageViewModel viewModel;
        public TypeOfCrossingsPage()
        {
            InitializeComponent();

            viewModel = (TypeOfCrossingsPageViewModel)this.BindingContext;

            bridgeCrossing.CheckedChanged += BridgeCrossing_CheckedChanged;
            railwayCrossing.CheckedChanged += RailwayCrossing_CheckedChanged;
            culvertCrossing.CheckedChanged += CulvertCrossing_CheckedChanged;
            roadCrossing.CheckedChanged += RoadCrossing_CheckedChanged;
            otherCrossing.CheckedChanged += OthersCrossing_CheckedChanged;
        }

        private void BridgeCrossing_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            if (sender == null)
            {
                return;
            }

            viewModel.EnableCrossingFlagsBlock("bridge", bridgeCrossing.Checked);
        }

        private void RailwayCrossing_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            if (sender == null)
            {
                return;
            }

            viewModel.EnableCrossingFlagsBlock("railway", railwayCrossing.Checked);
        }

        private void CulvertCrossing_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            if (sender == null)
            {
                return;
            }

            viewModel.EnableCrossingFlagsBlock("culvert", culvertCrossing.Checked);
        }

        private void RoadCrossing_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            if (sender == null)
            {
                return;
            }

            viewModel.EnableCrossingFlagsBlock("road", roadCrossing.Checked);
        }

        private void OthersCrossing_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            if (sender == null)
            {
                return;
            }

            viewModel.EnableCrossingFlagsBlock("others", otherCrossing.Checked);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
