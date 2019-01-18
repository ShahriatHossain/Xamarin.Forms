using MnMLilon.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MnMLilon.Service.Model;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MnMLilon.Service.Implementation
{
    public class TroubleTicketService : BaseService, ITroubleTicketService
    {
        public async Task<ObservableCollection<PendingTroubleTicketModel>> GetAllPendingTicketsAsync(string technicianNumber)
        {
            var action = "SiteBatteryTroubleTicketApi?technicianNumber=" + technicianNumber;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<PendingTroubleTicketModel>>(response);
        }

        public async Task<ObservableCollection<SparePartModel>> GetSpareParts()
        {
            var action = "SiteBatteryTroubleTicketApi/GetSpareParts";
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<SparePartModel>>(response);
        }

        public async Task<ObservableCollection<TroubleCategorySetupModel>> GetCategoryList()
        {
            var action = "SiteBatteryTroubleTicketApi/GetCategoryList";
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<TroubleCategorySetupModel>>(response);
        }

        public async Task<ObservableCollection<TroubleCategorySetupModel>> GetSubCategoryList()
        {
            var action = "SiteBatteryTroubleTicketApi/GetSubCategoryList";
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<TroubleCategorySetupModel>>(response);
        }

        public async Task<BatteryCommissionSiteModel> GetSiteDetails(int siteId)
        {
            var action = "SiteBatteryTroubleTicketApi/GetSiteDetails?siteId=" + siteId;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<BatteryCommissionSiteModel>(response);
        }

        public async Task<ResponseModel> UpdateTroubleTicket(TroubleTicketViewModel ticketModel)
        {
            var response = await PostMasterChildObjAndReadAsStringAsync("SiteBatteryTroubleTicketApi", true
                , ticketModel);

            return JsonConvert.DeserializeObject<ResponseModel>(response);
        }

        public async Task<TroubleTicketViewModel> GetTicketDetails(int ticketId)
        {
            var action = "SiteBatteryTroubleTicketApi/GetTicketDetails?ticketId=" + ticketId;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<TroubleTicketViewModel>(response);
        }
    }
}
