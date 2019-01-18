using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace MnMFiber.ViewModels.PatrollerDailySurveillance
{
    public class PatrollerDailySurveillancePageViewModel : BindableBase, IBaseInterface
    {
        private readonly INavigationService _navigationService;
        public PatrollerDailySurveillancePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            InitiatePageAsync();
        }


        private ObservableCollection<CustomCategory> _buttonList;
        public ObservableCollection<CustomCategory> ButtonList
        {
            get { return _buttonList; }
            set { SetProperty(ref _buttonList, value); }
        }

        internal async void GoToDetailPage(CustomCategory selectedItem)
        {
            switch (selectedItem.Id)
            {
                case 1:
                    await _navigationService.NavigateAsync("RouteMapPage", null, false, false);
                    break;

                case 2:
                    await _navigationService.NavigateAsync("", null, false, false);
                    break;
            }
        }

        public void InitiatePageAsync()
        {
            ButtonList = new ObservableCollection<CustomCategory>()
            {
                new CustomCategory { Id=1, Name="Route Map" },
                new CustomCategory { Id=1, Name="Start Patrolling" }
            };
        }

        public void SaveToSession()
        {

        }
    }
}
