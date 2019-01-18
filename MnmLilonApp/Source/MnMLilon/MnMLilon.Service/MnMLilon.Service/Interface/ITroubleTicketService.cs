using MnMLilon.Service.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Interface
{
    public interface ITroubleTicketService
    {
        Task<ObservableCollection<PendingTroubleTicketModel>> GetAllPendingTicketsAsync(string technicianNumber);
        Task<ObservableCollection<SparePartModel>> GetSpareParts();
        Task<ObservableCollection<TroubleCategorySetupModel>> GetCategoryList();
        Task<ObservableCollection<TroubleCategorySetupModel>> GetSubCategoryList();
        Task<BatteryCommissionSiteModel> GetSiteDetails(int siteId);
        Task<ResponseModel> UpdateTroubleTicket(TroubleTicketViewModel ticketModel);
        Task<TroubleTicketViewModel> GetTicketDetails(int ticketId);
    }
}
