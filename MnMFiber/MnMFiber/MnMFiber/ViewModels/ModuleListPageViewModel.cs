using MnMFiber.Common.Helper;
using MnMFiber.Core.Models;
using MnMFiber.Persistence;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMFiber.ViewModels
{
    public class ModuleListPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly Database database;
        public ModuleListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            database = new Database("User");
            database.CreateTable<User>();
        }


        DelegateCommand<string> _landingCommand;
        public DelegateCommand<string> LandingCommand => _landingCommand != null ?
            _landingCommand : (_landingCommand = new DelegateCommand<string>(GotoModuleLandingPage));

        private void GotoModuleLandingPage(string id)
        {
            switch (id)
            {
                case "1":
                    _navigationService.NavigateAsync("HotoTicketListPage", null, false, false);
                    break;

                case "2":
                    _navigationService.NavigateAsync("PatrollerDailySurveillancePage", null, false, false);
                    break;
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CommonHelper.CheckAllPluginsPermissionStatus();
        }

        DelegateCommand _changePasswordCommand;
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand != null ?
            _changePasswordCommand : (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));
        private async void ChangePasswordAsync()
        {
            await _navigationService.NavigateAsync("PasswordChangePage", null, false, false);
        }


        DelegateCommand _reLoginCommand;
        public DelegateCommand ReLoginCommand => _reLoginCommand != null ?
            _reLoginCommand : (_reLoginCommand = new DelegateCommand(ReLoginAsync));
        private async void ReLoginAsync()
        {
            CommonHelper.UnSubscribeTopicForNotification(UserSession.Current.UserId);

            database.DeleteAll<User>();

            HotoFiberSession.Current.SetHotoFiberSession(null);

            UserSession.Current.SetUserSession(null);

            await _navigationService.NavigateAsync("LoginPage", null, false, false);
        }
    }
}
