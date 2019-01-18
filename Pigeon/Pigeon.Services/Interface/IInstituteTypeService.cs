using Pigeon.Services.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.Services.Interface
{
    public interface IInstituteTypeService
    {
        Task<ObservableCollection<InstituteType>> GetInstituteTypesAsync();
    }
}
