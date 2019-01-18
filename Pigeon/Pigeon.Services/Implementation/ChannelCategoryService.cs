using Newtonsoft.Json;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public class ChannelCategoryService : BaseService, IChannelCategoryService
    {
        public async Task<ObservableCollection<ChannelCategory>> GetChannelCategoriesAsync()
        {
            var action = "channel/ChannelCategories";
            var response = await GetAndReadAsStringAsync(action, true);
            var categoryList = JsonConvert.DeserializeObject<ObservableCollection<ChannelCategory>>(response);
            if (categoryList != null)
                return categoryList;
            else
                return new ObservableCollection<ChannelCategory>();
        }
    }
}
