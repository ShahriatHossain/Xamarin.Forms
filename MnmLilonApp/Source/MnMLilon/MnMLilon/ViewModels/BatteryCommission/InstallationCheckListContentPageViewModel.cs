using System;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using MnMLilon.Service.Model;
using MnMLilon.Service.Configuration;
using Prism.Commands;

namespace MnMLilon.ViewModels.BatteryCommission
{
    public class InstallationCheckListContentPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        public InstallationCheckListContentPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private void SaveAsync()
        {
            _navigationService.GoBackAsync(null, false, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            TransactionId = BatteryCommissioningSession.Current.CommissionTransactionId;
            TransactionNumber = BatteryCommissioningSession.Current.CommissionTransactionNo;

            GetInstallationCheckList();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
           
        }

        private ObservableCollection<Category> _installationCheckList;
        public ObservableCollection<Category> InstallationCheckList
        {
            get { return _installationCheckList; }
            set { SetProperty(ref _installationCheckList, value); }
        }
        private void GetInstallationCheckList()
        {
            InstallationCheckList = new ObservableCollection<Category>
            {
                new Category { Id = 1, Name = "Mechanical Installation", Flag = BatteryCommissioningSession.Current.IsMechanicalInstallationFilled },
                new Category { Id = 2, Name = "Electrical Installation", Flag = BatteryCommissioningSession.Current.IsElectricalInstallationFilled }
            };
        }

        public async void NavigateToInstallationCheckFormAsync(Category category)
        {
            var param = new NavigationParameters();

            switch (category.Id)
            {
                case 1:
                    await _navigationService.NavigateAsync("MechanicalInstallationContentPage", null, false, false);
                    break;
                case 2:
                    await _navigationService.NavigateAsync("ElectricalInstallationContentPage", null, false, false);
                    break;
            }
        }
    }
}
