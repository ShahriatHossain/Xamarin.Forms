using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Tailoryfy.ViewModels
{
    public class LoginVerifyPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        public LoginVerifyPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        DelegateCommand _loginVerifyCommand;
        public DelegateCommand LoginVerifyCommand => _loginVerifyCommand ?? (_loginVerifyCommand = new DelegateCommand(VerifyLogin));

        private void VerifyLogin()
        {
            _navigationService.NavigateAsync("CategoryPage", null, false, false);
        }
    }
}
