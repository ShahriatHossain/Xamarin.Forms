using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public class ChannelService : BaseService, IChannelService
    {
        HttpClient client;
        public async Task<string> SaveChannelAsync(ChannelApi channel, bool isNew = false)
        {
            var response = await PostAndReadAsStringAsync("channel", true
                , CreateInput("Name", channel.Name)
                , CreateInput("ChannelCategoryId", channel.ChannelCategoryId)
                , CreateInput("InstituteId", channel.InstituteId)
                , CreateInput("Logo", channel.Logo));

            return response;
        }

        public async Task<string> UpdateChannelAsync(ChannelApi channel, bool isNew = false)
        {
            var response = await PostAndReadAsStringAsync("channel", true
                , CreateInput("Id", channel.ChannelId)
                , CreateInput("Name", channel.Name)
                , CreateInput("ChannelCategoryId", channel.ChannelCategoryId)
                , CreateInput("InstituteId", channel.InstituteId)
                , CreateInput("Logo", channel.Logo));

            return response;
        }

        public async Task<ObservableCollection<ChannelApi>> GetChannelByUserAsync(int userId, string cStatus)
        {
            var action = "channel?creatorId=" + userId + "&status=" + cStatus;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelList = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response);
            if (channelList != null)
                return channelList;
            else
                return new ObservableCollection<ChannelApi>();
        }

        public async Task<ObservableCollection<ChannelApi>> GetSubscribeChannelByUserAsync(string lastRefreshDate)
        {
            var response = await GetAndReadAsStringAsync("channel/subscribe?lastRefreshDate=" + lastRefreshDate, true);
            var channelList = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response);
            if (channelList != null)
                return channelList;
            else
                return new ObservableCollection<ChannelApi>();
        }

        public async Task<ObservableCollection<ChannelApi>> GetSubscribeAndOwnChannelByUserAsync()
        {
            var response = await GetAndReadAsStringAsync("channel/subscribe?includeOwnChannel=true", true);
            var channelList = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response);
            if (channelList != null)
                return channelList;
            else
                return new ObservableCollection<ChannelApi>();
        }

        public async Task<ChannelApi> GetChannelDetailAsync(int? channelId, string cStatus)
        {
            var action = "channel?channelId=" + channelId;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelDetail = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response).FirstOrDefault();
            if (channelDetail != null)
                return channelDetail;
            else
                return new ChannelApi();
        }

        public async Task<ChannelApi> GetChannelDetailByOwnedAsync(int? channelId, string cStatus)
        {
            var action = "channel?channelId=" + channelId + "&isOwnedByMe=" + true;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelDetail = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response).FirstOrDefault();
            if (channelDetail != null)
                return channelDetail;
            else
                return new ChannelApi();
        }

        public async Task<ChannelApi> GetChannelDetailBySubscribedAsync(int? channelId, string cStatus)
        {
            var action = "channel?channelId=" + channelId + "&isSubscribedByMe=" + true;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelDetail = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response).FirstOrDefault();
            if (channelDetail != null)
                return channelDetail;
            else
                return new ChannelApi();
        }

        public async Task<ChannelApi> GetChannelDetailByExcludeSubscribedAOwnedAsync(int? channelId, string cStatus)
        {
            var action = "channel?channelId=" + channelId + "&isSubscribedByMe=" + false + "&isOwnedByMe=" + false;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelDetail = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response).FirstOrDefault();
            if (channelDetail != null)
                return channelDetail;
            else
                return new ChannelApi();
        }

        public async Task<string> GetSecurePinCodeAsync(int? channelId)
        {
            ChannelApi channel = await GetChannelDetailAsync(channelId, string.Empty);
            return channel.SecurePin;
        }

        public async Task<string> SaveSecurePinCodeAsync(int? channelId, string securePin)
        {
            var uri = "channel/" + channelId + "/securepin/" + securePin;
            var response = await PutAndReadAsStringAsync(uri, true);

            return response;
        }

        public async Task<ObservableCollection<ChannelApi>> SearchChannelAsync(int userId, string searchText)
        {
            var action = "channel?creatorId=" + userId + "&searchText=" + searchText;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelList = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response);
            if (channelList != null)
                return channelList;
            else
                return new ObservableCollection<ChannelApi>();
        }

        public async Task<bool> SubscribeChannelAsync(int? channelId)
        {
            var response = await PostAndReadAsStringAsync("channel/subscribe/" + channelId, true);
            var jObject = JsonConvert.DeserializeObject<JObject>(response);

            return true;
        }

        public async Task<bool> UnSubscribeChannelAsync(int? channelId)
        {
            var response = await PostAndReadAsStringAsync("channel/unsubscribe/" + channelId, true);
            var jObject = JsonConvert.DeserializeObject<JObject>(response);

            return true;
        }

        public async Task<ObservableCollection<ChannelApi>> GetChannelByInstituteAsync(int instituteId)
        {
            var action = "channel?instituteId=" + instituteId;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelList = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response);
            if (channelList != null)
                return channelList;
            else
                return new ObservableCollection<ChannelApi>();
        }

        public async Task<ObservableCollection<ChannelApi>> GetChannelByInstituteByDefault(int instituteId, bool isDefault)
        {
            var action = "channel?instituteId=" + instituteId + "&isDefault=" + isDefault;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelList = JsonConvert.DeserializeObject<ObservableCollection<ChannelApi>>(response);
            if (channelList != null)
                return channelList;
            else
                return new ObservableCollection<ChannelApi>();
        }

        public async Task<string> PostAndGetBlobAsync(dynamic file, string fileType)
        {
            var response = await UploadAndReadAsStringAsync("channel/PostAndGetBlob", true, file, fileType);
            //var jObject = JsonConvert.DeserializeObject<JObject>(response);
            return response;
        }

        public async Task<ObservableCollection<ChannelSubscribe>> GetSubscribers(int channelId)
        {
            var action = "channel/subscribers/" + channelId;
            var response = await GetAndReadAsStringAsync(action, true);
            var subscribeList = JsonConvert.DeserializeObject<ObservableCollection<ChannelSubscribe>>(response);
            if (subscribeList != null)
                return subscribeList;
            else
                return new ObservableCollection<ChannelSubscribe>();
        }

    }
}
