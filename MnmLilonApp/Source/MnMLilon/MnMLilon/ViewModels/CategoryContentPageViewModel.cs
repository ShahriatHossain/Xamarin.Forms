using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.DataAccess.Implementation;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace MnMLilon.ViewModels
{
    public class CategoryContentPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        public CategoryContentPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        private int _rowHeight;
        public int RowHeight
        {
            get { return _rowHeight; }
            set { SetProperty(ref _rowHeight, value); }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CheckRequiredPermissionEnabled();

            GetAllCategory();

            var screenHeight = App.ScreenHeight;
            RowHeight = screenHeight / 2;
        }


        private void CheckRequiredPermissionEnabled()
        {
            CommonHelper.EnableAllRequiredDevicePermissions();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }


        private ObservableCollection<Category> _categoryList;
        public ObservableCollection<Category> CategoryList
        {
            get { return _categoryList; }
            set { SetProperty(ref _categoryList, value); }
        }
        private void GetAllCategory()
        {
            CategoryList = new ObservableCollection<Category>
            {
                new Category { Id = 1, Name = "Battery Commissioning", Flag = BatteryCommissioningSession.Current.IsNewMessageArrived },
                new Category { Id = 2, Name = "PM / Complaint", Flag = TroubleTicketSession.Current.IsNewMessageArrived }
            };
        }


        public async void NavigateToCommissionTransactionListAsync(Category category)
        {
            switch (category.Id)
            {
                case 1:
                    await _navigationService.NavigateAsync("CommissionTransactionListContentPage", null, false, false);
                    break;
                case 2:
                    await _navigationService.NavigateAsync("TroubleTicketTransactionListContentPage", null, false, false);
                    break;
            }
        }


        DelegateCommand _changePasswordCommand;
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand != null ?
            _changePasswordCommand : (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));
        private async void ChangePasswordAsync()
        {
            await _navigationService.NavigateAsync("PasswordChangeContentPage", null, false, false);
        }


        DelegateCommand _reLoginCommand;
        public DelegateCommand ReLoginCommand => _reLoginCommand != null ?
            _reLoginCommand : (_reLoginCommand = new DelegateCommand(ReLoginAsync));
        private async void ReLoginAsync()
        {
            CommonHelper.UnSubscribeTopicForNotification(UserSession.Current.UserId);

            UserHelper.DeleteAllUsers();
            await _navigationService.NavigateAsync("LoginContentPage", null, false, false);
        }
    }
}
