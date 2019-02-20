using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tailoryfy.Core.Interfaces;
using Tailoryfy.Core.Models;

namespace Tailoryfy.Services
{
    public class ProductStepService : IProductStepService
    {
        public ObservableCollection<ProductStep> GetAll()
        {
            var productStepList = new ObservableCollection<ProductStep>();

            var sampleList = PopulateSampleProductStepList();

            foreach (var ps in sampleList)
            {
                productStepList.Add(ps);
            }

            return productStepList;
        }

        private IList<ProductStep> PopulateSampleProductStepList()
        {
            var productStepList = new List<ProductStep>()
            {
                new ProductStep
                {
                    Id = 1,
                    ProductCategoryId = 1,
                    Name = "Front",
                    Image = "StepBar.png",
                    IsSelected = true
                },
                new ProductStep
                {
                    Id = 1,
                    ProductCategoryId = 1,
                    Name = "Back",
                    Image = "StepBar.png",
                    IsSelected = false
                },
                new ProductStep
                {
                    Id = 1,
                    ProductCategoryId = 1,
                    Name = "Sleeve",
                    Image = "StepBar.png",
                    IsSelected = false
                },
                new ProductStep
                {
                    Id = 1,
                    ProductCategoryId = 1,
                    Name = "Style",
                    Image = "StepBar.png",
                    IsSelected = false
                },
                new ProductStep
                {
                    Id = 1,
                    ProductCategoryId = 1,
                    Name = "Add-ons",
                    Image = "StepBar.png",
                    IsSelected = false
                }
            };

            return productStepList;
        }
    }
}
