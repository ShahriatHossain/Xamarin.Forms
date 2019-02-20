using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tailoryfy.Core.Interfaces;
using Tailoryfy.Core.Models;

namespace Tailoryfy.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        public ObservableCollection<ProductCategory> GetAll()
        {
            var productCategoryList = new ObservableCollection<ProductCategory>();

            var sampleList = PopulateSampleCategoryList();
            foreach (var category in sampleList)
            {
                productCategoryList.Add(category);
            }

            return productCategoryList;
        }

        private IList<ProductCategory> PopulateSampleCategoryList()
        {
            var productCategoryList = new List<ProductCategory>()
            {
                new ProductCategory
                {
                    Id = 1,
                    Name = "Salwar Suit",
                    Description = "Personalise your product by adding necklines, " +
                    "sleeves and other add-ons of your choice from 100's of styles",
                    BgColor = "#FFFFFF",
                    Image = "salwar_suit.png"
                },
                new ProductCategory
                {
                    Id = 1,
                    Name = "Bluse",
                    Description = "Personalise your product by adding necklines, " +
                    "sleeves and other add-ons of your choice from 100's of styles",
                    BgColor = "#F6F4F4",
                    Image = "blouse.png"
                },
                new ProductCategory
                {
                    Id = 1,
                    Name = "Bottom",
                    Description = "Personalise your product by adding necklines, " +
                    "sleeves and other add-ons of your choice from 100's of styles",
                    BgColor = "#FFFFFF",
                    Image = "bottom.png"
                },
                new ProductCategory
                {
                    Id = 1,
                    Name = "Anarkali",
                    Description = "Personalise your product by adding necklines, " +
                    "sleeves and other add-ons of your choice from 100's of styles",
                    BgColor = "#F6F4F4",
                    Image = "anarkali.png"
                }
            };

            return productCategoryList;
        }
    }
}
