using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class SiteDetailFormListContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _batteryCommissionService;
        private readonly IFileUploadService _fileUploadService;
        public SiteDetailFormListContentPageViewModel(INavigationService navigationService, IBatteryCommissionService batteryCommissionService,
            IFileUploadService fileUploadService)
        {
            _navigationService = navigationService;
            _batteryCommissionService = batteryCommissionService;
            _fileUploadService = fileUploadService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                TransactionId = BatteryCommissioningSession.Current.CommissionTransactionId;
                TransactionNumber = BatteryCommissioningSession.Current.CommissionTransactionNo;

                CheckFormsHasBeenFilled();

                GetSiteDetailFormList();
            }
            catch (Exception ex)
            {

            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }


        private ObservableCollection<Category> _siteDetailFormList;
        public ObservableCollection<Category> SiteDetailFormList
        {
            get { return _siteDetailFormList; }
            set { SetProperty(ref _siteDetailFormList, value); }
        }
        private void GetSiteDetailFormList()
        {
            SiteDetailFormList = new ObservableCollection<Category>
            {
                new Category { Id = 1, Name = "Site General Information", Flag = IsSiteGeneralInformationFilled},
                new Category { Id = 2, Name = "Battery General Information", Flag =  IsBatteryGeneralInformationFilled},
                new Category { Id = 3, Name = "Site Status", Flag = IsSiteStatusFilled },
                new Category { Id = 4, Name = "Check List Before Installation", Flag = IsCheckListBeforeInstallationFilled },
                new Category { Id = 5, Name = "Installation Check List", Flag = IsInstallationCheckListFilled },
                new Category { Id = 6, Name = "Battery Bank Data Information", Flag = IsBatteryBankDataInformationFilled },
                new Category { Id = 7, Name = "Functional Test on Battery", Flag = IsFunctionalTestOnBatteryFilled },
                new Category { Id = 8, Name = "Photos & Signature", Flag = IsPhotosAndSignatureFilled }
            };
        }

        public async void NavigateToSiteDetailFormAsync(Category category)
        {
            var param = new NavigationParameters();

            switch (category.Id)
            {
                case 1:
                    await _navigationService.NavigateAsync("SiteGeneralInformationContentPage", null, false, false);
                    break;
                case 2:
                    await _navigationService.NavigateAsync("BatteryGeneralInformationContentPage", null, false, false);
                    break;
                case 3:
                    await _navigationService.NavigateAsync("SiteStatusContentPage", null, false, false);
                    break;
                case 4:
                    await _navigationService.NavigateAsync("CheckListBeforeInstallationContentPage", null, false, false);
                    break;
                case 5:
                    await _navigationService.NavigateAsync("InstallationCheckListContentPage", null, false, false);
                    break;
                case 6:
                    await _navigationService.NavigateAsync("BatteryListContentPage", null, false, false);
                    break;
                case 7:
                    await _navigationService.NavigateAsync("FunctionalTestOnBatteryContentPage", null, false, false);
                    break;
                case 8:
                    await _navigationService.NavigateAsync("PhotosAndSignatureContentPage", null, false, false);
                    break;
            }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));
        private async void SaveAsync()
        {
            try
            {
                await CommonHelper.ShowLoaderAsync();

                var transaction = BatteryCommissioningSession.Current.Transaction;

                transaction.UpdatedByNumber = UserSession.Current.UserName;



                transaction.TransactionDetailsModel.Photo1 = (BatteryCommissioningSession.Current.Photo1 == null) ? transaction.TransactionDetailsModel.Photo1 : await _fileUploadService.UploadFileToServerAsync(BatteryCommissioningSession.Current.Photo1);
                transaction.TransactionDetailsModel.Photo2 = (BatteryCommissioningSession.Current.Photo2 == null) ? transaction.TransactionDetailsModel.Photo2 : await _fileUploadService.UploadFileToServerAsync(BatteryCommissioningSession.Current.Photo2);

                transaction.TechnicianSign = (BatteryCommissioningSession.Current.Signature1 == null) ? transaction.TechnicianSign : await _fileUploadService.UploadFileToServerAsync(BatteryCommissioningSession.Current.Signature1);
                transaction.CustomerContactPersonSign = (BatteryCommissioningSession.Current.Signature2 == null) ? transaction.CustomerContactPersonSign : await _fileUploadService.UploadFileToServerAsync(BatteryCommissioningSession.Current.Signature2);

                transaction.TransactionDetailsModel.TransactionDocument = (BatteryCommissioningSession.Current.File1 == null) ? transaction.TransactionDetailsModel.TransactionDocument : await _fileUploadService.UploadFileToServerAsync(BatteryCommissioningSession.Current.File1);

                var result = await _batteryCommissionService.UpdateTransactionDetail(transaction);

                if (result != null && result.Success == true)
                {
                    ClearCurrentSession();

                    CommonHelper.DisplayAlertMessage("", "Successfully Saved.");

                    await _navigationService.NavigateAsync("CommissionTransactionListContentPage", null, false, false);
                }

                await CommonHelper.HideLoaderAsync();
            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();

                await _navigationService.NavigateAsync("CommissionTransactionListContentPage", null, false, false);
            }

        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(CancelAsync));
        private async void CancelAsync()
        {
            try
            {
                var isCancelled = await CommonHelper.AlertMessageWithCancelOrOkAsync("", "Do you want to cancel?");
                if (isCancelled)
                {
                    await CommonHelper.ShowLoaderAsync();

                    ClearCurrentSession();

                    await CommonHelper.HideLoaderAsync();

                    await _navigationService.NavigateAsync("CommissionTransactionListContentPage", null, false, false);
                }
            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();
            }

        }


        private static void ClearCurrentSession()
        {
            BatteryCommissioningSession.Current.SetBatteryCommissioningSession(null);
        }

        private bool _isSiteGeneralInformationFilled;
        public bool IsSiteGeneralInformationFilled
        {
            get { return _isSiteGeneralInformationFilled; }
            set { SetProperty(ref _isSiteGeneralInformationFilled, value); }
        }

        private bool _isBatteryGeneralInformationFilled;
        public bool IsBatteryGeneralInformationFilled
        {
            get { return _isBatteryGeneralInformationFilled; }
            set { SetProperty(ref _isBatteryGeneralInformationFilled, value); }
        }

        private bool _isSiteStatusFilled;
        public bool IsSiteStatusFilled
        {
            get { return _isSiteStatusFilled; }
            set { SetProperty(ref _isSiteStatusFilled, value); }
        }

        private bool _isCheckListBeforeInstallationFilled;
        public bool IsCheckListBeforeInstallationFilled
        {
            get { return _isCheckListBeforeInstallationFilled; }
            set { SetProperty(ref _isCheckListBeforeInstallationFilled, value); }
        }

        private bool _isMechanicalInstallationFilled;
        public bool IsMechanicalInstallationFilled
        {
            get { return _isMechanicalInstallationFilled; }
            set { SetProperty(ref _isMechanicalInstallationFilled, value); }
        }

        private bool _isElectricalInstallationFilled;
        public bool IsElectricalInstallationFilled
        {
            get { return _isElectricalInstallationFilled; }
            set { SetProperty(ref _isElectricalInstallationFilled, value); }
        }

        private bool _isBatteryBankDataInformationFilled;
        public bool IsBatteryBankDataInformationFilled
        {
            get { return _isBatteryBankDataInformationFilled; }
            set { SetProperty(ref _isBatteryBankDataInformationFilled, value); }
        }

        private bool _isFunctionalTestOnBatteryFilled;
        public bool IsFunctionalTestOnBatteryFilled
        {
            get { return _isFunctionalTestOnBatteryFilled; }
            set { SetProperty(ref _isFunctionalTestOnBatteryFilled, value); }
        }

        private bool _isPhotosAndSignatureFilled;
        public bool IsPhotosAndSignatureFilled
        {
            get { return _isPhotosAndSignatureFilled; }
            set { SetProperty(ref _isPhotosAndSignatureFilled, value); }
        }

        private bool _isInstallationCheckListFilled;
        public bool IsInstallationCheckListFilled
        {
            get { return _isInstallationCheckListFilled; }
            set { SetProperty(ref _isInstallationCheckListFilled, value); }
        }

        private bool _isAllFormsHasBeenFilled;
        public bool IsAllFormsHasBeenFilled
        {
            get { return _isAllFormsHasBeenFilled; }
            set { SetProperty(ref _isAllFormsHasBeenFilled, value); }
        }
        private void CheckFormsHasBeenFilled()
        {
            IsSiteGeneralInformationFilled = BatteryCommissioningSession.Current.IsSiteGeneralInformationFilled;

            IsBatteryGeneralInformationFilled = BatteryCommissioningSession.Current.IsBatteryGeneralInformationFilled;

            IsSiteStatusFilled = BatteryCommissioningSession.Current.IsSiteStatusFilled;

            IsCheckListBeforeInstallationFilled = BatteryCommissioningSession.Current.IsCheckListBeforeInstallationFilled;

            IsElectricalInstallationFilled = BatteryCommissioningSession.Current.IsElectricalInstallationFilled;

            IsBatteryBankDataInformationFilled = BatteryCommissioningSession.Current.IsBatteryBankDataInformationFilled;

            IsFunctionalTestOnBatteryFilled = BatteryCommissioningSession.Current.IsFunctionalTestOnBatteryFilled;

            IsPhotosAndSignatureFilled = BatteryCommissioningSession.Current.IsPhotosAndSignatureFilled;

            if (IsMechanicalInstallationFilled || IsElectricalInstallationFilled)
            {
                IsInstallationCheckListFilled = true;
            }

            if (IsSiteGeneralInformationFilled && IsBatteryGeneralInformationFilled && IsSiteStatusFilled && IsCheckListBeforeInstallationFilled
                && IsBatteryBankDataInformationFilled && IsFunctionalTestOnBatteryFilled && IsPhotosAndSignatureFilled && IsInstallationCheckListFilled)
            {
                IsAllFormsHasBeenFilled = true;
            }
        }
    }
}
