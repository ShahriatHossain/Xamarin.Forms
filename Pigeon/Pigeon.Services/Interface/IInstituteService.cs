using Pigeon.Services.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.Services.Interface
{
    public interface IInstituteService
    {
        Task<string> SaveInstituteAsync(Institute institute);
        Task<string> UpdateInstituteAsync(Institute institute);
        Task<ObservableCollection<Institute>> GetInstituteByUserAsync(int userId);
        Task<ObservableCollection<Institute>> GetSubscribeInstituteByUserAsync();
        Task<ObservableCollection<Institute>> GetSubscribeAndOwnInstituteByUserAsync();
        Task<Institute> GetInstituteDetailAsync(int? instituteId);
        Task<ObservableCollection<Institute>> GetInstituteBySearchTextAsync(int userId, string searchText, bool includeSubscribed);
        Task<bool> SubscribeInstituteAsync(int? instituteId);
        Task<bool> UnSubscribeInstituteAsync(int? instituteId);
        Task<Institute> GetInstituteDetailByOwnedAsync(int? instituteId);
        Task<Institute> GetInstituteDetailBySubscribedAsync(int? instituteId);
        Task<Institute> GetInstituteDetailByExcludeSubscribedAOwnedAsync(int? instituteId);
        Task<ObservableCollection<InstituteSubscribe>> GetSubscribers(int instituteId);
    }
}
