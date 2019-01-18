using Microsoft.Practices.Unity;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Models;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence;
using MnMFiber.Persistence.Sessions;
using MnMFiber.Services;
using MnMFiber.Views;
using MnMFiber.Views.HotoFiber;
using MnMFiber.Views.PatrollerDailySurveillance;
using Prism.Unity;
using System.Linq;
using Xamarin.Forms;

namespace MnMFiber
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var database = new Database("User");
            database.CreateTable<User>();

            var user = database.GetItems<User>().ToList().FirstOrDefault();
            if (user != null && user.ID > 0)
            {
                UserSession.Current.SetUserNumber(user.UserName);
                NavigationService.NavigateAsync("NavigationPage/ModuleListPage");
            }
            else
                NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<ModuleListPage>();
            Container.RegisterTypeForNavigation<HotoTicketListPage>();
            Container.RegisterTypeForNavigation<HotoTicketDetailPage>();
            Container.RegisterTypeForNavigation<AddSpotPage>();
            Container.RegisterTypeForNavigation<TypeOfSpotsPage>();
            Container.RegisterTypeForNavigation<FiberDetailsPage>();
            Container.RegisterTypeForNavigation<CableDetailPage>();
            Container.RegisterTypeForNavigation<TypeOfCrossingsPage>();
            Container.RegisterTypeForNavigation<AerialCablePage>();
            Container.RegisterTypeForNavigation<OtherDetailPage>();
            Container.RegisterTypeForNavigation<ViewSpotsPage>();
            Container.RegisterTypeForNavigation<PasswordChangePage>();
            Container.RegisterTypeForNavigation<ChambersPage>();
            Container.RegisterTypeForNavigation<SignaturesPage>();
            Container.RegisterTypeForNavigation<SignatureDetailPage>();
            Container.RegisterTypeForNavigation<PatrollerDailySurveillancePage>();
            UnityContainerExtensions.RegisterType<IUserService, UserService>(Container);
            UnityContainerExtensions.RegisterType<IHotoFiberService, HotoFiberService>(Container);
            UnityContainerExtensions.RegisterType<IPatrollerDailySurveillanceService, PatrollerDailySurveillanceService>(Container);
            Container.RegisterTypeForNavigation<RouteMapPage>();
        }
    }
}
