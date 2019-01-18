using Newtonsoft.Json;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public class InstituteTypeService : BaseService, IInstituteTypeService
    {
        public async Task<ObservableCollection<InstituteType>> GetInstituteTypesAsync()
        {
            var action = "institute/InstituteTypes";
            var response = await GetAndReadAsStringAsync(action, true);
            var typeList = JsonConvert.DeserializeObject<ObservableCollection<InstituteType>>(response);
            if (typeList != null)
                return typeList;
            else
                return new ObservableCollection<InstituteType>();
        }
    }
}
