using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Tailoryfy.ViewModels
{
    public class LoginPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        private void Login()
        {
            _navigationService.NavigateAsync("LoginVerifyPage", null, false, false);
        }
    }
}
