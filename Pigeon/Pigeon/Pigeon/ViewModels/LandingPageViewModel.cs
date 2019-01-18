using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace Pigeon.ViewModels
{
    public class LandingPageViewModel : BindableBase
    {
        private ImageSource _appLogo;
        public ImageSource AppLogo
        {
            get { return _appLogo; }
            set { SetProperty(ref _appLogo, value); }
        }

        DelegateCommand _viewIndividualRegPage;
        public DelegateCommand ViewIndividualRegPage => _viewIndividualRegPage != null ?
            _viewIndividualRegPage : (_viewIndividualRegPage = new DelegateCommand(NavigateToIndivisualRegPageAsync));

        private async void NavigateToIndivisualRegPageAsync()
        {
            await _navigationService.NavigateAsync("UserRegistrationPage", null, false, false);
        }

        private readonly INavigationService _navigationService;

        public LandingPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AppLogo = ImageSource.FromResource("Pigeon.Images.icon.png");
        }
    }
}
