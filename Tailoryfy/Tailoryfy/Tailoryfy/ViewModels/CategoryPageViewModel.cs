using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Tailoryfy.Core.Models;

namespace Tailoryfy.ViewModels
{
    public class CategoryPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        public CategoryPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitiatePage();
        }


        private ObservableCollection<CustomCategory> _categoryList;
        public ObservableCollection<CustomCategory> CategoryList
        {
            get { return _categoryList; }
            set { SetProperty(ref _categoryList, value); }
        }

        private void InitiatePage()
        {
            CategoryList = new ObservableCollection<CustomCategory>()
            {
                new CustomCategory()
                {
                    Id = 1,
                    Name = "Tailoring Service at Your fingertips",
                    BgColor = "#F6F4F4",
                    Image = "category1.png"
                },
                new CustomCategory()
                {
                    Id = 2,
                    Name = "At-Home Alteration Service For Men & Women",
                    BgColor = "#FFFFFF",
                    Image = "category2.png"
                }
            };
        }

        public void GoToDetails(CustomCategory category)
        {
            _navigationService.NavigateAsync("ProductCategoryPage", null, false, false);
        }
    }
}
