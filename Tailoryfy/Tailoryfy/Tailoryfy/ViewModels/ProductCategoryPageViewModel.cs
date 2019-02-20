using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Tailoryfy.Core.Interfaces;
using Tailoryfy.Core.Models;

namespace Tailoryfy.ViewModels
{
    public class ProductCategoryPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryPageViewModel(INavigationService navigationService, IProductCategoryService productCategoryService)
        {
            _navigationService = navigationService;
            _productCategoryService = productCategoryService;

            InitiatePage();
        }


        private ObservableCollection<ProductCategory> _productCategory;
        public ObservableCollection<ProductCategory> ProductCategoryList
        {
            get { return _productCategory; }
            set { SetProperty(ref _productCategory, value); }
        }
        private void InitiatePage()
        {
            ProductCategoryList = _productCategoryService.GetAll();
        }

        internal void GoToDetails(ProductCategory productCategory)
        {
            var param = new NavigationParameters();
            param.Add("productCategory", productCategory);
            _navigationService.NavigateAsync("ProductDetailsPage", param, false, false);
        }
    }
}
