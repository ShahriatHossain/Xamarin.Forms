using MnMLilon.Service.Configuration;
using MnMLilon.Service.Implementation;
using MnMLilon.Service.Model;
using MnMLilon.ViewModels.BatteryCommission;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MnMLilon.Views.BatteryCommission
{
    public partial class SiteGeneralInformationContentPage : ContentPage
    {
        private SiteGeneralInformationContentPageViewModel _viewModel;
        public SiteGeneralInformationContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            _viewModel = (SiteGeneralInformationContentPageViewModel)this.BindingContext;

            SetDefaultPickerValueAsync();

            siteConfiguration.SelectedIndexChanged += SiteConfiguration_SelectedIndexChanged;

            FocusFieldsAfterCompletion();
        }

        private void FocusFieldsAfterCompletion()
        {
            siteMake.Completed += (sender, e) =>
            {
                siteModel.Focus();
            };

            siteModel.Completed += (sender, e) =>
            {
                siteCapacity.Focus();
            };

            siteCapacity.Completed += (sender, e) =>
            {
                powerPlantSoftwareVersion.Focus();
            };
        }

        private async void SetDefaultPickerValueAsync()
        {
            var siteConfigList = await new BatteryCommissionService().GetAllSiteConfigAsync();
            siteConfiguration.ItemsSource = siteConfigList;

            var siteConfigId = BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.SiteConfigId;

            if(siteConfigId > 0)
            {
                foreach(var item in siteConfigList)
                {
                    if(item.Val == siteConfigId.ToString())
                    {
                        siteConfiguration.SelectedItem = item;
                    }
                }
            }
        }

        private void SiteConfiguration_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var siteConfig = (SiteConfig)siteConfiguration.SelectedItem;
            _viewModel.SiteConfigId = Convert.ToInt32(siteConfig.Val);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}
