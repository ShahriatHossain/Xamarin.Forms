using Core.Models;
using System.Collections.ObjectModel;

namespace Core.Interfaces
{
    public interface IProductCategoryService
    {
        ObservableCollection<ProductCategory> GetAll();
    }
}
