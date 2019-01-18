using MMTowers.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMTowers.Service.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace MMTowers.Service.Implementation
{
    public class TowerService : BaseService, ITowerService
    {
        async Task<ObservableCollection<Tower>> ITowerService.GetAllByTechnicianAsync(string technicianNumber)
        {
            var action = "DieselRefill?technicianNumber=" + technicianNumber;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<Tower>>(response);
        }

        public async Task<Tower> GetDetailAsync(int id)
        {
            var action = "DieselRefill/" + id;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<Tower>(response);
        }


    }
}
