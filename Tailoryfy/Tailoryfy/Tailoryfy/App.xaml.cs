using DLToolkit.Forms.Controls;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Tailoryfy.Core.Interfaces;
using Tailoryfy.Services;
using Tailoryfy.Views;
using Xamarin.Forms;


namespace Tailoryfy
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            FlowListView.Init();

            NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<LoginVerifyPage>();
            Container.RegisterTypeForNavigation<CategoryPage>();
            Container.RegisterTypeForNavigation<ProductCategoryPage>();
            Container.RegisterTypeForNavigation<ProductDetailsPage>();
            UnityContainerExtensions.RegisterType<IProductCategoryService, ProductCategoryService>(Container);
            UnityContainerExtensions.RegisterType<IProductStepService, ProductStepService>(Container);
            UnityContainerExtensions.RegisterType<IProductStepAttachment, ProductStepAttachmentService>(Container);
        }
    }
}
