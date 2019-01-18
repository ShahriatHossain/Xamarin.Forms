using MnMFiber.Common.Helper;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence;
using MnMFiber.Persistence.Sessions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMFiber.ViewModels
{
    public class PasswordChangePageViewModel : BindableBase, IBaseInterface
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userRepository;
        private readonly Database database;

        public PasswordChangePageViewModel(INavigationService navigationService, IUserService userRepository)
        {
            _navigationService = navigationService;
            _userRepository = userRepository;

            database = new Database("User");
            database.CreateTable<User>();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            InitiatePageAsync();
        }

        private UserAccount _userAccount;
        public UserAccount UserAccount
        {
            get { return _userAccount; }
            set { SetProperty(ref _userAccount, value); }
        }

        public void InitiatePageAsync()
        {
            UserAccount = new UserAccount { };
        }

        public void SaveToSession()
        {

        }

        DelegateCommand _changePasswordCommand;
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand != null ?
            _changePasswordCommand : (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));

        private async void ChangePasswordAsync()
        {
            if (string.IsNullOrEmpty(UserAccount.OldPassword)
                || string.IsNullOrEmpty(UserAccount.Password)
                || string.IsNullOrEmpty(UserAccount.ConfirmPassword))
            {
                CommonHelper.DisplayAlertMessage("", "All those fields required.");
            }
            else if (!UserAccount.Password.Equals(UserAccount.ConfirmPassword))
            {
                CommonHelper.DisplayAlertMessage("", "Confirm password doesn't match.");
            }
            else
            {
                UserAccount.UserName = UserSession.Current.UserNumber;

                var response = await _userRepository.UpdatePasswordAsync(UserAccount);

                database.DeleteAll<User>();

                CommonHelper.UnSubscribeTopicForNotification(UserSession.Current.UserId);

                HotoFiberSession.Current.SetHotoFiberSession(null);

                UserSession.Current.SetUserSession(null);

                CommonHelper.DisplayAlertMessage("", "Successfully updated.");

                await _navigationService.NavigateAsync("LoginPage", null, false, false);
            }
        }

        private DelegateCommand _navToHomeCommand;
        public DelegateCommand NavToHomeCommand => _navToHomeCommand != null ? _navToHomeCommand :
            (_navToHomeCommand = new DelegateCommand(NavToHomeAsync));

        private async void NavToHomeAsync()
        {
            await _navigationService.NavigateAsync("ModuleListPage", null, false, false);
        }
    }
}
