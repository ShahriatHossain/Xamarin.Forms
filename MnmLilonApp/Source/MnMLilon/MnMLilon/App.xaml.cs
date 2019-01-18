using Prism.Unity;
using MnMLilon.Views;
using Xamarin.Forms;
using MnMLilon.Service.Implementation;
using Microsoft.Practices.Unity;
using MnMLilon.Service.Interface;
using MnMLilon.Service.DataAccess.Implementation;
using MnMLilon.Service.Configuration;
using MnMLilon.Views.TroubleTicket;
using MnMLilon.Views.BatteryCommission;

namespace MnMLilon
{
    public partial class App : PrismApplication
    {
        public static int ScreenWidth;
        public static int ScreenHeight;

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            
            var user = UserHelper.GetUser();

            if (user != null && user.ID > 0)
            {
                UserSession.Current.SetUserName(user.UserName);

                NavigationService.NavigateAsync("NavigationPage/CategoryContentPage");
            }
            else
            {
                NavigationService.NavigateAsync("NavigationPage/LoginContentPage");
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<LoginContentPage>();
            Container.RegisterTypeForNavigation<CategoryContentPage>();
            Container.RegisterTypeForNavigation<CommissionTransactionListContentPage>();
            UnityContainerExtensions.RegisterType<IUserService, UserService>(Container);
            UnityContainerExtensions.RegisterType<IFileUploadService, FileUploadService>(Container);
            UnityContainerExtensions.RegisterType<IBatteryCommissionService, BatteryCommissionService>(Container);
            UnityContainerExtensions.RegisterType<ITroubleTicketService, TroubleTicketService>(Container);
            Container.RegisterTypeForNavigation<SiteDetailFormListContentPage>();
            Container.RegisterTypeForNavigation<SiteGeneralInformationContentPage>();
            Container.RegisterTypeForNavigation<PasswordChangeContentPage>();
            Container.RegisterTypeForNavigation<BatteryGeneralInformationContentPage>();
            Container.RegisterTypeForNavigation<SiteStatusContentPage>();
            Container.RegisterTypeForNavigation<CheckListBeforeInstallationContentPage>();
            Container.RegisterTypeForNavigation<FunctionalTestOnBatteryContentPage>();
            Container.RegisterTypeForNavigation<BatteryBankDataInformationContentPage>();
            Container.RegisterTypeForNavigation<InstallationCheckListContentPage>();
            Container.RegisterTypeForNavigation<MechanicalInstallationContentPage>();
            Container.RegisterTypeForNavigation<ElectricalInstallationContentPage>();
            Container.RegisterTypeForNavigation<PhotosAndSignatureContentPage>();
            Container.RegisterTypeForNavigation<SignatureContentPage>();
            Container.RegisterTypeForNavigation<BatteryListContentPage>();
            Container.RegisterTypeForNavigation<TroubleTicketTransactionListContentPage>();

            Container.RegisterTypeForNavigation<TroubleTicketTabListContentPage>();
            Container.RegisterTypeForNavigation<EquipmentDetailsPage>();
            Container.RegisterTypeForNavigation<SiteDetailsPage>();
            Container.RegisterTypeForNavigation<DetailsOfWorkDonePage>();
            Container.RegisterTypeForNavigation<FunctionalTestOnBatteryPage>();
            Container.RegisterTypeForNavigation<SignaturePage>();
            Container.RegisterTypeForNavigation<BatteryDetailsPage>();
            Container.RegisterTypeForNavigation<PowerPlantOtherDetailsPage>();
            Container.RegisterTypeForNavigation<SignPage>();
            Container.RegisterTypeForNavigation<MaterialsDetailPage>();
            Container.RegisterTypeForNavigation<GeneralInformationPage>();
            Container.RegisterTypeForNavigation<EditBatteryPopupPage>();
            Container.RegisterTypeForNavigation<FaultyAssetsPage>();
        }
    }
}
