using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace MnMLilon.Service.Implementation
{
    public class BatteryCommissionService : BaseService, IBatteryCommissionService
    {
        public async Task<ObservableCollection<SiteConfig>> GetAllSiteConfigAsync()
        {
            var action = "BatteryComission/GetAllSiteConfig";
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<SiteConfig>>(response);
        }

        public async Task<ObservableCollection<TechnicianTransaction>> GetAllTransactionsAsync(string technicianNumber)
        {
            var action = "BatteryComission?technicianNumber=" + technicianNumber;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<TechnicianTransaction>>(response);
        }

        public async Task<SiteDetail> GetSiteDetails(int id)
        {
            var action = "BatteryComission/GetSiteDetails?transactionId=" + id;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<SiteDetail>(response);
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            var action = "BatteryComission/GetTransaction?id=" + id;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<Transaction>(response);
        }

        public async Task<ResponseModel> UpdateTransactionDetail(Transaction transaction)
        {
            var response = await PostMasterChildObjAndReadAsStringAsync("BatteryComission", true
                , transaction);

            return JsonConvert.DeserializeObject<ResponseModel>(response);
        }
    }
}
