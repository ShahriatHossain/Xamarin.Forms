using System.Collections.ObjectModel;
using Tailoryfy.Core.Models;

namespace Tailoryfy.Core.Interfaces
{
    public interface IProductStepAttachment
    {
        ObservableCollection<ProductStepAttachment> GetAll();
        ObservableCollection<ProductStepAttachment> GetByProductStepId(int productStepId);
    }
}
