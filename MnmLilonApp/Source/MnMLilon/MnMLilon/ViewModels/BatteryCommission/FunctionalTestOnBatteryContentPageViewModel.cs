using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using Prism.Commands;
using Prism.Navigation;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class FunctionalTestOnBatteryContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IBatteryCommissionService _commissionService;
        public FunctionalTestOnBatteryContentPageViewModel(INavigationService navigationService, IBatteryCommissionService commissionService)
        {
            _navigationService = navigationService;
            _commissionService = commissionService;
        }

        private bool _communicationTestDone;
        public bool CommunicationTestDone
        {
            get { return _communicationTestDone; }
            set { SetProperty(ref _communicationTestDone, value); }
        }

        private bool _functionalTestDone;
        public bool FunctionalTestDone
        {
            get { return _functionalTestDone; }
            set { SetProperty(ref _functionalTestDone, value); }
        }

        private string _communicationTestComment;
        public string CommunicationTestComment
        {
            get { return _communicationTestComment; }
            set { SetProperty(ref _communicationTestComment, value); }
        }

        private string _functionalTestComment;
        public string FunctionalTestComment
        {
            get { return _functionalTestComment; }
            set { SetProperty(ref _functionalTestComment, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            BatteryCommissioningSession.Current.SetIsFunctionalTestOnBatteryFilled(true);

            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.CommunicationTestDone = CommunicationTestDone;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.FunctionalTestDone = FunctionalTestDone;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.CommunicationTestComment = CommunicationTestComment;
            BatteryCommissioningSession.Current.Transaction.TransactionDetailsModel.FunctionalTestComment = FunctionalTestComment;
            
            _navigationService.GoBackAsync(null, false, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetFieldsDefaultValueAsync();
        }

        private void SetFieldsDefaultValueAsync()
        {
            var transaction = BatteryCommissioningSession.Current.Transaction;

            CommunicationTestDone = transaction.TransactionDetailsModel.CommunicationTestDone;
            FunctionalTestDone = transaction.TransactionDetailsModel.FunctionalTestDone;
            CommunicationTestComment = transaction.TransactionDetailsModel.CommunicationTestComment;
            FunctionalTestComment = transaction.TransactionDetailsModel.FunctionalTestComment;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}
