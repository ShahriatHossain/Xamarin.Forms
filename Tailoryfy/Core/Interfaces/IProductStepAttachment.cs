using Core.Models;
using System.Collections.ObjectModel;

namespace Core.Interfaces
{
    public interface IProductStepAttachment
    {
        ObservableCollection<ProductStepAttachment> GetAll();
        ObservableCollection<ProductStepAttachment> GetByProductStepId(int productStepId);
    }
}
