using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class TroubleTicketTabListContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly ITroubleTicketService _troubleTicketService;
        private readonly IFileUploadService _fileUploadService;
        public TroubleTicketTabListContentPageViewModel(INavigationService navigationService, ITroubleTicketService troubleTicketService, IFileUploadService fileUploadService)
        {
            _navigationService = navigationService;
            _troubleTicketService = troubleTicketService;
            _fileUploadService = fileUploadService;
        }


        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        private ObservableCollection<Category> _tabList;
        public ObservableCollection<Category> TabList
        {
            get { return _tabList; }
            set { SetProperty(ref _tabList, value); }
        }
        private void GetTabList()
        {
            TabList = new ObservableCollection<Category>
            {
                new Category { Id = 1, Name = "General Information", Flag = TroubleTicketSession.Current.IsGeneralInformationFilled},
                new Category { Id = 2, Name = "Equipment Details", Flag = TroubleTicketSession.Current.IsEquipmentDetailsFilled},
                new Category { Id = 3, Name = "Site Details", Flag =  TroubleTicketSession.Current.IsSiteDetailsFilled},
                new Category { Id = 4, Name = "Details of Work Done", Flag = TroubleTicketSession.Current.IsDetailsofWorkDoneFilled },
                new Category { Id = 5, Name = "Faulty Assets", Flag = TroubleTicketSession.Current.IsFaultyAssetsSelected },
                new Category { Id = 6, Name = "Functional Test on Battery", Flag = TroubleTicketSession.Current.IsFunctionalTestonBatteryFilled },
                new Category { Id = 7, Name = "Signature & Remarks", Flag = TroubleTicketSession.Current.IsSignatureFilled }
            };
        }

        private PendingTroubleTicketModel _pendingTroubleTicket;
        public PendingTroubleTicketModel PendingTroubleTicket
        {
            get { return _pendingTroubleTicket; }
            set { SetProperty(ref _pendingTroubleTicket, value); }
        }
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GetTabList();

            PendingTroubleTicket = TroubleTicketSession.Current.PendingTroubleTicket;

            SetSiteDetail();

            CheckFormsHasBeenFilled();
        }

        private string _ticketNo;
        public string TicketNo
        {
            get { return _ticketNo; }
            set { SetProperty(ref _ticketNo, value); }
        }

        private string _siteID;
        public string SiteID
        {
            get { return _siteID; }
            set { SetProperty(ref _siteID, value); }
        }

        private string _siteName;
        public string SiteName
        {
            get { return _siteName; }
            set { SetProperty(ref _siteName, value); }
        }

        private string _siteAddress;
        public string SiteAddress
        {
            get { return _siteAddress; }
            set { SetProperty(ref _siteAddress, value); }
        }

        private string _siteProblem;
        public string SiteProblem
        {
            get { return _siteProblem; }
            set { SetProperty(ref _siteProblem, value); }
        }

        private string _typeOfComplaint;
        public string TypeOfComplaint
        {
            get { return _typeOfComplaint; }
            set { SetProperty(ref _typeOfComplaint, value); }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private string _subCategory;
        public string SubCatgory
        {
            get { return _subCategory; }
            set { SetProperty(ref _subCategory, value); }
        }

        private void SetSiteDetail()
        {
            if (PendingTroubleTicket != null)
            {
                var troubleTicketModel = TroubleTicketSession.Current.TroubleTicket;

                TicketNo = string.Format(@"{0}/{1}", PendingTroubleTicket.TicketNo, troubleTicketModel.ComplaintType);
                SiteID = string.Format(@"Site ID: {0}", PendingTroubleTicket.SiteID);
                SiteName = string.Format(@"Site Name: {0}", PendingTroubleTicket.SiteName);
                Category = string.Format(@"Category: {0}", troubleTicketModel.Category);
                SubCatgory = string.Format(@"Sub-Catgory: {0}", troubleTicketModel.SubCategory);
                SiteAddress = PendingTroubleTicket.SiteAddress;
                SiteProblem = string.Format(@"Problem: {0}", troubleTicketModel.ProblemReportedByCustomer);
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

                var ticketModel = TroubleTicketSession.Current.TroubleTicket;
                ticketModel.Id = TroubleTicketSession.Current.TroubleTicketId;

                ticketModel.TroubleTicketDetails.AssignedPersonSign = (TroubleTicketSession.Current.Signature1 == null) ? ticketModel.TroubleTicketDetails.AssignedPersonSign : await _fileUploadService.TroubleTicketUploadFileToServerAsync(TroubleTicketSession.Current.Signature1);
                ticketModel.TroubleTicketDetails.CustomerRepresentativeSign = (TroubleTicketSession.Current.Signature2 == null) ? ticketModel.TroubleTicketDetails.CustomerRepresentativeSign : await _fileUploadService.TroubleTicketUploadFileToServerAsync(TroubleTicketSession.Current.Signature2);

                ticketModel.TroubleTicketDetails.TicketDocument = (TroubleTicketSession.Current.File1 == null) ? ticketModel.TroubleTicketDetails.TicketDocument : await _fileUploadService.TroubleTicketUploadFileToServerAsync(TroubleTicketSession.Current.File1);

                var result = await _troubleTicketService.UpdateTroubleTicket(ticketModel);

                if (result != null && result.Success)
                {
                    ClearCurrentSession();

                    CommonHelper.DisplayAlertMessage("", "Successfully Saved.");

                    await _navigationService.NavigateAsync("TroubleTicketTransactionListContentPage", null, false, false);
                }

                await CommonHelper.HideLoaderAsync();
            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();

                await _navigationService.NavigateAsync("TroubleTicketTransactionListContentPage", null, false, false);
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

                    await _navigationService.NavigateAsync("TroubleTicketTransactionListContentPage", null, false, false);
                }
            }
            catch (Exception ex)
            {
                await CommonHelper.HideLoaderAsync();
            }

        }

        private static void ClearCurrentSession()
        {
            TroubleTicketSession.Current.SetTroubleTicketSession(null);
        }


        private bool _isAllFormsHasBeenFilled;
        public bool IsAllFormsHasBeenFilled
        {
            get { return _isAllFormsHasBeenFilled; }
            set { SetProperty(ref _isAllFormsHasBeenFilled, value); }
        }

        private void CheckFormsHasBeenFilled()
        {
            var IsGeneralInformationFilled = TroubleTicketSession.Current.IsGeneralInformationFilled;
            var IsEquipmentDetailsFilled = TroubleTicketSession.Current.IsEquipmentDetailsFilled;
            var IsDetailsofWorkDoneFilled = TroubleTicketSession.Current.IsDetailsofWorkDoneFilled;
            var IsFunctionalTestonBatteryFilled = TroubleTicketSession.Current.IsFunctionalTestonBatteryFilled;
            var IsSignatureFilled = TroubleTicketSession.Current.IsSignatureFilled;
            var IsSiteDetailsFilled = TroubleTicketSession.Current.IsSiteDetailsFilled;
            var IsFaultyAssetsSelected = TroubleTicketSession.Current.IsFaultyAssetsSelected;


            if (IsGeneralInformationFilled && IsEquipmentDetailsFilled && IsDetailsofWorkDoneFilled
                && IsFunctionalTestonBatteryFilled
                && IsSignatureFilled && IsSiteDetailsFilled && IsFaultyAssetsSelected)
            {
                IsAllFormsHasBeenFilled = true;
            }
        }


        public async void NavigateToTabDetailAsync(Category category)
        {
            var param = new NavigationParameters();

            switch (category.Id)
            {
                case 1:
                    await _navigationService.NavigateAsync("GeneralInformationPage", null, false, false);
                    break;
                case 2:
                    await _navigationService.NavigateAsync("EquipmentDetailsPage", null, false, false);
                    break;
                case 3:
                    await _navigationService.NavigateAsync("SiteDetailsPage", null, false, false);
                    break;
                case 4:
                    await _navigationService.NavigateAsync("DetailsOfWorkDonePage", null, false, false);
                    break;
                case 5:
                    await _navigationService.NavigateAsync("FaultyAssetsPage", null, false, false);
                    break;
                case 6:
                    await _navigationService.NavigateAsync("FunctionalTestOnBatteryPage", null, false, false);
                    break;
                case 7:
                    await _navigationService.NavigateAsync("SignaturePage", null, false, false);
                    break;
            }
        }
    }
}
