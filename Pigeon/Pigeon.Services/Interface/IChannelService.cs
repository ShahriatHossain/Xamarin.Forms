using Pigeon.Services.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.Services.Interface
{
    public interface IChannelService
    {
        Task<string> SaveChannelAsync(ChannelApi channel, bool isNew = false);
        Task<string> UpdateChannelAsync(ChannelApi channel, bool isNew = false);
        Task<ObservableCollection<ChannelApi>> GetChannelByUserAsync(int userId, string cStatus);
        Task<ChannelApi> GetChannelDetailAsync(int? channelId, string cStatus);
        Task<string> GetSecurePinCodeAsync(int? channelId);
        Task<string> SaveSecurePinCodeAsync(int? channelId, string securePin);
        Task<ObservableCollection<ChannelApi>> SearchChannelAsync(int userId, string searchText);
        Task<bool> SubscribeChannelAsync(int? channelId);
        Task<bool> UnSubscribeChannelAsync(int? channelId);
        //Task<ObservableCollection<ChannelNotice>> GetChannelNoticeAsync(int? channelId, int sequenceId);
        Task<ObservableCollection<ChannelApi>> GetSubscribeChannelByUserAsync(string lastRefreshDate);
        Task<ObservableCollection<ChannelApi>> GetSubscribeAndOwnChannelByUserAsync();
        Task<ObservableCollection<ChannelApi>> GetChannelByInstituteAsync(int instituteId);
        Task<string> PostAndGetBlobAsync(dynamic file, string fileType);
        Task<ObservableCollection<ChannelApi>> GetChannelByInstituteByDefault(int instituteId, bool isDefault);
        Task<ChannelApi> GetChannelDetailByOwnedAsync(int? channelId, string cStatus);
        Task<ChannelApi> GetChannelDetailBySubscribedAsync(int? channelId, string cStatus);
        Task<ChannelApi> GetChannelDetailByExcludeSubscribedAOwnedAsync(int? channelId, string cStatus);
        Task<ObservableCollection<ChannelSubscribe>> GetSubscribers(int channelId);
    }
}
