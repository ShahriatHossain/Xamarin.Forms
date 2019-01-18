using MnMLilon.Service.Configuration;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class EquipmentDetailsPageViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;
        public EquipmentDetailsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GetTabList();
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
                new Category { Id = 1, Name = "Battery Details", Flag = TroubleTicketSession.Current.IsBatteryDetailsFilled},
                new Category { Id = 2, Name = "Powerplant & Setting Details", Flag =  TroubleTicketSession.Current.IsPowerPlantNOtherDetailsFilled}
            };
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }


        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsEquipmentDetailsFilled(true);

            await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
        }


        public async void NavigateToTabDetailAsync(Category category)
        {
            var param = new NavigationParameters();

            switch (category.Id)
            {
                case 1:
                    await _navigationService.NavigateAsync("BatteryDetailsPage", null, false, false);
                    break;
                case 2:
                    await _navigationService.NavigateAsync("PowerPlantOtherDetailsPage", null, false, false);
                    break;
            }
        }
    }
}
