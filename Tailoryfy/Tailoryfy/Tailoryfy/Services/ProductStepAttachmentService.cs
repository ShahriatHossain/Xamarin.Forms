using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tailoryfy.Core.Interfaces;
using Tailoryfy.Core.Models;

namespace Tailoryfy.Services
{
    public class ProductStepAttachmentService : IProductStepAttachment
    {
        public ObservableCollection<ProductStepAttachment> GetAll()
        {
            var productStepAttachmentList = new ObservableCollection<ProductStepAttachment>();

            var sampleList = PopulateSampleProductStepAttachmentList();

            foreach (var ps in sampleList)
            {
                productStepAttachmentList.Add(ps);
            }

            return productStepAttachmentList;
        }

        public ObservableCollection<ProductStepAttachment> GetByProductStepId(int productStepId)
        {
            var productStepAttachmentList = new ObservableCollection<ProductStepAttachment>();

            var sampleList = PopulateSampleProductStepAttachmentList()
                .Where(a => a.ProductStepId == productStepId)
                .ToList();

            foreach (var ps in sampleList)
            {
                productStepAttachmentList.Add(ps);
            }

            return productStepAttachmentList;
        }

        private IList<ProductStepAttachment> PopulateSampleProductStepAttachmentList()
        {
            var productStepAttachmentList = new List<ProductStepAttachment>()
            {
                new ProductStepAttachment
                {
                    Id = 1,
                    ProductStepId = 1,
                    Name = "U-Neck",
                    IsSelected = false,
                    Image = "Neck01.png"
                },
                new ProductStepAttachment
                {
                    Id = 2,
                    ProductStepId = 1,
                    Name = "Crew Neck",
                    IsSelected = false,
                    Image = "Neck02.png"
                },
                new ProductStepAttachment
                {
                    Id = 3,
                    ProductStepId = 1,
                    Name = "Boat Neck",
                    IsSelected = false,
                    Image = "Neck03.png"
                },
                new ProductStepAttachment
                {
                    Id = 4,
                    ProductStepId = 2,
                    Name = "Peter Pan Collar",
                    IsSelected = false,
                    Image = "Back01.png"
                },
                new ProductStepAttachment
                {
                    Id = 5,
                    ProductStepId = 2,
                    Name = "Stand-up Collar with Sweetheart neck",
                    IsSelected = false,
                    Image = "Neck05.png"
                },

                new ProductStepAttachment
                {
                    Id = 6,
                    ProductStepId = 2,
                    Name = "U-Neck",
                    IsSelected = false,
                    Image = "Neck01.png"
                },
                new ProductStepAttachment
                {
                    Id = 7,
                    ProductStepId = 3,
                    Name = "Crew Neck",
                    IsSelected = false,
                    Image = "Sleeve01.png"
                },
                new ProductStepAttachment
                {
                    Id = 8,
                    ProductStepId = 3,
                    Name = "Boat Neck",
                    IsSelected = false,
                    Image = "Sleeve02.png"
                },
                new ProductStepAttachment
                {
                    Id = 9,
                    ProductStepId = 3,
                    Name = "Peter Pan Collar",
                    IsSelected = false,
                    Image = "Sleeve03.png"
                },
                new ProductStepAttachment
                {
                    Id = 10,
                    ProductStepId = 4,
                    Name = "Crew Neck",
                    IsSelected = false,
                    Image = "Neck02.png"
                },
                new ProductStepAttachment
                {
                    Id = 11,
                    ProductStepId = 4,
                    Name = "Boat Neck",
                    IsSelected = false,
                    Image = "Neck03.png"
                },
                new ProductStepAttachment
                {
                    Id = 12,
                    ProductStepId = 4,
                    Name = "Peter Pan Collar",
                    IsSelected = false,
                    Image = "Neck04.png"
                },
                new ProductStepAttachment
                {
                    Id = 13,
                    ProductStepId = 5,
                    Name = "Crew Neck",
                    IsSelected = false,
                    Image = "AddOn01.png"
                },
                new ProductStepAttachment
                {
                    Id = 14,
                    ProductStepId = 5,
                    Name = "Boat Neck",
                    IsSelected = false,
                    Image = "AddOn02.png"
                },
                new ProductStepAttachment
                {
                    Id = 15,
                    ProductStepId = 5,
                    Name = "Peter Pan Collar",
                    IsSelected = false,
                    Image = "AddOn03.png"
                },
                new ProductStepAttachment
                {
                    Id = 16,
                    ProductStepId = 5,
                    Name = "Crew Neck",
                    IsSelected = false,
                    Image = "AddOn04.png"
                }
            };

            return productStepAttachmentList;
        }
    }
}
