using Core.Models;
using System.Collections.ObjectModel;

namespace Core.Interfaces
{
    public interface IProductStepService
    {
        ObservableCollection<ProductStep> GetAll();
    }
}
