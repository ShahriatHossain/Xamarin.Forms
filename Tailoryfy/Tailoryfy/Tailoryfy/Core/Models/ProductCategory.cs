using System.Collections.ObjectModel;

namespace Tailoryfy.Core.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BgColor { get; set; }
        public string Image { get; set; }

        public ObservableCollection<ProductStep> ProductStepList { get; set; }
            = new ObservableCollection<ProductStep>();
    }
}