using System.Collections.ObjectModel;
using Tailoryfy.Core.Models;

namespace Tailoryfy.Core.Interfaces
{
    public interface IProductCategoryService
    {
        ObservableCollection<ProductCategory> GetAll();
    }
}
