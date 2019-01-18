using Prism.Mvvm;
using Prism.Navigation;

namespace Pigeon.ViewModels
{
    public class PopUpForInstituteFabButtonsViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        public PopUpForInstituteFabButtonsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public async void CreateInstitute()
        {
            await _navigationService.NavigateAsync("CreateInstitutePage", null, false, false);
        }

        public async void DiscoverInstitute()
        {
            await _navigationService.NavigateAsync("InstituteDiscoverPage", null, false, false);
        }
    }
}
