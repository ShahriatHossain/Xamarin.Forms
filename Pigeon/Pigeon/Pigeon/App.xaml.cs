using Microsoft.Practices.Unity;
using Pigeon.Services.Implementation;
using Pigeon.Services.Interface;
using Pigeon.Views;
using Prism.Unity;
using Xamarin.Forms;

namespace Pigeon
{
    public partial class App : PrismApplication
    {
        public static double ScreenWidth;
        public static double ScreenHeight;

        public App()
        {
            //MainPage = new NavigationPage(new ChannelViewer());
            // check is otp has been verified or set
            var isUserVerified = new UserService().IsVerifiedOtp();
            var isOtpSent = new UserService().IsOtpSent();

            if (isOtpSent)
            {
                MainPage = new NavigationPage(new OtpVerifyPage());
            }
            else if (!isUserVerified)
            {
                // The root page of your application
                MainPage = new NavigationPage(new UserRegistrationPage());
            }
            else
            {
                // The root page of your application
                MainPage = new NavigationPage(new InstituteTabbedPage());
            }

        }

        public int ResumeAtTodoId { get; set; }

        public static bool IsUserLoggedIn { get; set; }

        protected override void OnInitialized()
        {
            InitializeComponent();
            //UserSession.Current.SetLastRefreshDatetime(System.DateTime.Now.ToString("dd/M/yyyy HH:mm:ss"));
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<ChannelCreatePage>();
            Container.RegisterTypeForNavigation<ChannelOptionPage>();
            Container.RegisterTypeForNavigation<ChannelViewer>();
            Container.RegisterTypeForNavigation<ChannelDetailViewer>();
            Container.RegisterTypeForNavigation<UserRegistrationPage>();
            Container.RegisterTypeForNavigation<OtpVerifyPage>();
            Container.RegisterTypeForNavigation<MessageVoteResultPage>();
            Container.RegisterTypeForNavigation<ChannelSecureIdPage>();
            Container.RegisterTypeForNavigation<ChannelSearchPage>();
            Container.RegisterTypeForNavigation<ChannelSubscribePage>();
            Container.RegisterTypeForNavigation<VerifySecureIdPage>();
            Container.RegisterTypeForNavigation<ChannelUnSubscribePage>();
            Container.RegisterTypeForNavigation<ChannelSettingPage>();
            Container.RegisterTypeForNavigation<MobileNoChangePage>();
            Container.RegisterTypeForNavigation<NoticeCreatePage>();
            Container.RegisterTypeForNavigation<MyChannelListPage>();
            Container.RegisterTypeForNavigation<DisplayLargeImagePage>();
            Container.RegisterTypeForNavigation<PDFViewerContentPage>();
            Container.RegisterTypeForNavigation<FaqPage>();
            Container.RegisterTypeForNavigation<LandingPage>();
            Container.RegisterTypeForNavigation<ChannelListPage>();
            Container.RegisterTypeForNavigation<InstituteTabbedPage>();
            Container.RegisterTypeForNavigation<InstituteListPage>();
            Container.RegisterTypeForNavigation<CreateInstitutePage>();
            Container.RegisterTypeForNavigation<InstituteChannelList>();
            Container.RegisterTypeForNavigation<InstituteDiscoverPage>();
            Container.RegisterTypeForNavigation<PopUpForInstituteFabButtons>();
            Container.RegisterTypeForNavigation<SubscriberListPage>();


            UnityContainerExtensions.RegisterType<IUserService, UserService>(Container);
            UnityContainerExtensions.RegisterType<IMessageService, MessageService>(Container);
            UnityContainerExtensions.RegisterType<IChannelService, ChannelService>(Container);
            UnityContainerExtensions.RegisterType<IInstituteService, InstituteService>(Container);
            UnityContainerExtensions.RegisterType<IInstituteCategoryService, InstituteCategoryService>(Container);
            UnityContainerExtensions.RegisterType<IInstituteTypeService, InstituteTypeService>(Container);
            UnityContainerExtensions.RegisterType<IChannelCategoryService, ChannelCategoryService>(Container);
            Container.RegisterTypeForNavigation<ChannelEditPage>();
            Container.RegisterTypeForNavigation<EditInstitutePage>();
        }

    }
}
