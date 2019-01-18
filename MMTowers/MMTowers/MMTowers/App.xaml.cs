using Prism.Unity;
using MMTowers.Views;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using MMTowers.Service.Interface;
using MMTowers.Service.Implementation;
using MMTowers.Service.Common;
using MMTowers.Helper;

namespace MMTowers
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var user = DatabaseHelper.GetUser();
            
            if(user != null && user.ID > 0)
            {
                UserSession.Current.SetUserName(user.UserName);

                NavigationService.NavigateAsync("NavigationPage/TowerListPage");
            }
            else
            {
                NavigationService.NavigateAsync("NavigationPage/LoginPage");
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<TowerListPage>();
            Container.RegisterTypeForNavigation<DieselRefillPage>();
            UnityContainerExtensions.RegisterType<IAccountService, AccountService>(Container);
            UnityContainerExtensions.RegisterType<ITowerService, TowerService>(Container);
            UnityContainerExtensions.RegisterType<IDieselRefillService, DieselRefillService>(Container);
            Container.RegisterTypeForNavigation<PasswordChangePage>();
        }
    }
}
