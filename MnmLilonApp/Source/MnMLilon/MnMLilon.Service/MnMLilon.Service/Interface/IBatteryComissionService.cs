using MnMLilon.Service.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Interface
{
    public interface IBatteryCommissionService
    {
        Task<ObservableCollection<TechnicianTransaction>> GetAllTransactionsAsync(string technicianNumber);
        Task<SiteDetail> GetSiteDetails(int id);
        Task<ResponseModel> UpdateTransactionDetail(Transaction transaction);
        Task<Transaction> GetTransactionAsync(int id);
        Task<ObservableCollection<SiteConfig>> GetAllSiteConfigAsync();
    }
}
