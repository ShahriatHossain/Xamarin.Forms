using System.Collections.ObjectModel;

namespace Tailoryfy.Core.Models
{
    public class ProductStep
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public ObservableCollection<ProductStepAttachment> ProductStepAttachmentList { get; set; }
            = new ObservableCollection<ProductStepAttachment>();
    }
}
