using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Tailoryfy.Core.Interfaces;
using Tailoryfy.Core.Models;

namespace Tailoryfy.ViewModels
{
    public class ProductDetailsPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IProductStepService _productStepService;
        private readonly IProductStepAttachment _productStepAttachment;

        public ProductDetailsPageViewModel(INavigationService navigationService, IProductStepService productStepService
            , IProductStepAttachment productStepAttachment)
        {
            _navigationService = navigationService;
            _productStepService = productStepService;
            _productStepAttachment = productStepAttachment;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var productCategory = (ProductCategory)parameters["productCategory"];
            InitiatePage(productCategory);
        }


        private ObservableCollection<ProductStep> _productStepList;
        public ObservableCollection<ProductStep> ProductStepList
        {
            get { return _productStepList; }
            set { SetProperty(ref _productStepList, value); }
        }

        private ProductCategory _productCategory;
        public ProductCategory ProductCategory
        {
            get { return _productCategory; }
            set { SetProperty(ref _productCategory, value); }
        }

        private ObservableCollection<ProductStepAttachment> _productStepAttachmentList;
        public ObservableCollection<ProductStepAttachment> ProductStepAttachmentList
        {
            get { return _productStepAttachmentList; }
            set { SetProperty(ref _productStepAttachmentList, value); }
        }

        private ProductStep _selectedProductStep = new ProductStep();
        public ProductStep SelectedProductStep
        {
            get { return _selectedProductStep; }
            set { SetProperty(ref _selectedProductStep, value); }
        }
        private void InitiatePage(ProductCategory productCategory)
        {
            ProductCategory = productCategory;

            ProductStepList = _productStepService.GetAll();

            ProductStepAttachmentList = _productStepAttachment.GetByProductStepId(ProductStepList[0].Id);

            SelectedProductStep = ProductStepList[0];
        }
    }
}
