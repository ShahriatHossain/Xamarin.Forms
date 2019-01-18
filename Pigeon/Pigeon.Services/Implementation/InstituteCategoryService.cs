using Newtonsoft.Json;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public class InstituteCategoryService : BaseService, IInstituteCategoryService
    {
        public async Task<ObservableCollection<InstituteCategory>> GetInstituteCategoriesAsync()
        {
            var action = "institute/InstituteCategories";
            var response = await GetAndReadAsStringAsync(action, true);
            var categoryList = JsonConvert.DeserializeObject<ObservableCollection<InstituteCategory>>(response);
            if (categoryList != null)
                return categoryList;
            else
                return new ObservableCollection<InstituteCategory>();
        }
    }
}
