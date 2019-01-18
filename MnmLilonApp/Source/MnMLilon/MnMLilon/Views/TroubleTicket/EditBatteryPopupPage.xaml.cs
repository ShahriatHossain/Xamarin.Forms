using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.ViewModels.TroubleTicket;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MnMLilon.Views.TroubleTicket
{
    public partial class EditBatteryPopupPage : PopupPage
    {
        private EditBatteryPopupPageViewModel _viewModel;
        private int BatteryId;
        public EditBatteryPopupPage(int batteryId)
        {
            InitializeComponent();

            _viewModel = (EditBatteryPopupPageViewModel)this.BindingContext;

            BatteryId = batteryId;

            btnConfirm.Clicked += BtnConfirm_ClickedAsync;

            btnCancel.Clicked += BtnCancel_Clicked;

            SetDefaultFieldValues();
        }

        private void SetDefaultFieldValues()
        {
            var batteryList = TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatteryList;
            var batteryDetail = batteryList.Find(c => c.Id == BatteryId);

            oldSrNo.Text = batteryDetail.BatterySerialNo;
            newSrNo.Text = batteryDetail.BatterySerialNo;
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void BtnConfirm_ClickedAsync(object sender, EventArgs e)
        {
            //var isConfirm = await CommonHelper.AlertMessageWithCancelOrOkAsync(string.Empty, "Are you confirm to change it?");
            //if (!isConfirm)
            //{
            //    return;
            //}

            if (string.IsNullOrEmpty(newSrNo.Text))
            {
                CommonHelper.DisplayAlertMessage(string.Empty, "New Sr. No. Field can't be empty.");
                return;
            }

            var itemIndex = TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatteryList.FindIndex(x => x.Id == BatteryId);
            var item = TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatteryList.Find(x => x.Id == BatteryId);

            TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatteryList.RemoveAt(itemIndex);
            item.BatterySerialNo = newSrNo.Text;
            TroubleTicketSession.Current.TroubleTicket.SiteDetails.BatteryList.Insert(itemIndex, item);

            PopupNavigation.Instance.PopAsync();

            await _viewModel.NavigateToBatteryDetailPageAsync();
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }
    }
}
