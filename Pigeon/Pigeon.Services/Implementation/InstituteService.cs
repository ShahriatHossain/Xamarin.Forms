using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public class InstituteService : BaseService, IInstituteService
    {
        public async Task<string> SaveInstituteAsync(Institute institute)
        {
            var response = await PostAndReadAsStringAsync("institute", true
                , CreateInput("Name", institute.Name)
                , CreateInput("Area", institute.Area)
                , CreateInput("District", institute.District)
                , CreateInput("Division", institute.Division)
                , CreateInput("Description", institute.Description)
                , CreateInput("InstituteCategoryId", institute.InstituteCategoryId)
                , CreateInput("InstituteTypeId", institute.InstituteTypeId)
                , CreateInput("Logo", institute.Logo));

            return response;
        }

        public async Task<string> UpdateInstituteAsync(Institute institute)
        {
            var response = await PostAndReadAsStringAsync("institute", true
                , CreateInput("Id", institute.InstituteId)
                , CreateInput("Name", institute.Name)
                , CreateInput("Area", institute.Area)
                , CreateInput("District", institute.District)
                , CreateInput("Division", institute.Division)
                , CreateInput("Description", institute.Description)
                , CreateInput("InstituteCategoryId", institute.InstituteCategoryId)
                , CreateInput("InstituteTypeId", institute.InstituteTypeId)
                , CreateInput("Logo", institute.Logo));

            return response;
        }

        public async Task<ObservableCollection<Institute>> GetInstituteByUserAsync(int userId)
        {
            var action = "institute?creatorId=" + userId;
            var response = await GetAndReadAsStringAsync(action, true);
            var instituteList = JsonConvert.DeserializeObject<ObservableCollection<Institute>>(response);
            if (instituteList != null)
                return instituteList;
            else
                return new ObservableCollection<Institute>();
        }

        public async Task<ObservableCollection<Institute>> GetInstituteBySearchTextAsync(
            int userId
            , string searchText
            , bool includeSubscribed)
        {
            var action = "institute?creatorId=" + userId + "&searchText=" + searchText + "&isSubscribedByMe=" + includeSubscribed;
            var response = await GetAndReadAsStringAsync(action, true);
            var instituteList = JsonConvert.DeserializeObject<ObservableCollection<Institute>>(response);
            if (instituteList != null)
                return instituteList;
            else
                return new ObservableCollection<Institute>();
        }


        public async Task<ObservableCollection<Institute>> GetSubscribeInstituteByUserAsync()
        {
            var response = await GetAndReadAsStringAsync("institute/subscribe", true);
            var channelList = JsonConvert.DeserializeObject<ObservableCollection<Institute>>(response);
            if (channelList != null)
                return channelList;
            else
                return new ObservableCollection<Institute>();
        }

        public async Task<ObservableCollection<Institute>> GetSubscribeAndOwnInstituteByUserAsync()
        {
            var response = await GetAndReadAsStringAsync("institute/subscribe?includeOwnChannel=true", true);
            var channelList = JsonConvert.DeserializeObject<ObservableCollection<Institute>>(response);
            if (channelList != null)
                return channelList;
            else
                return new ObservableCollection<Institute>();
        }

        public async Task<bool> SubscribeInstituteAsync(int? instituteId)
        {
            var response = await PostAndReadAsStringAsync("institute/subscribe/" + instituteId, true);
            var jObject = JsonConvert.DeserializeObject<JObject>(response);

            return true;
        }

        public async Task<bool> UnSubscribeInstituteAsync(int? instituteId)
        {
            var response = await PostAndReadAsStringAsync("institute/unsubscribe/" + instituteId, true);
            var jObject = JsonConvert.DeserializeObject<JObject>(response);

            return true;
        }

        public async Task<Institute> GetInstituteDetailAsync(int? instituteId)
        {
            var action = "institute?instituteId=" + instituteId;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelDetail = JsonConvert.DeserializeObject<ObservableCollection<Institute>>(response).FirstOrDefault();
            if (channelDetail != null)
                return channelDetail;
            else
                return new Institute();
        }

        public async Task<Institute> GetInstituteDetailByOwnedAsync(int? instituteId)
        {
            var action = "institute?instituteId=" + instituteId + "&isOwnedByMe=" + true;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelDetail = JsonConvert.DeserializeObject<ObservableCollection<Institute>>(response).FirstOrDefault();
            if (channelDetail != null)
                return channelDetail;
            else
                return new Institute();
        }

        public async Task<Institute> GetInstituteDetailBySubscribedAsync(int? instituteId)
        {
            var action = "institute?instituteId=" + instituteId + "&isSubscribedByMe=" + true;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelDetail = JsonConvert.DeserializeObject<ObservableCollection<Institute>>(response).FirstOrDefault();
            if (channelDetail != null)
                return channelDetail;
            else
                return new Institute();
        }

        public async Task<Institute> GetInstituteDetailByExcludeSubscribedAOwnedAsync(int? instituteId)
        {
            var action = "institute?instituteId=" + instituteId + "&isSubscribedByMe=" + false + "&isOwnedByMe=" + false;
            var response = await GetAndReadAsStringAsync(action, true);
            var channelDetail = JsonConvert.DeserializeObject<ObservableCollection<Institute>>(response).FirstOrDefault();
            if (channelDetail != null)
                return channelDetail;
            else
                return new Institute();
        }

        public async Task<ObservableCollection<InstituteSubscribe>> GetSubscribers(int instituteId)
        {
            var action = "institute/subscribers/" + instituteId;
            var response = await GetAndReadAsStringAsync(action, true);
            var subscribeList = JsonConvert.DeserializeObject<ObservableCollection<InstituteSubscribe>>(response);
            if (subscribeList != null)
                return subscribeList;
            else
                return new ObservableCollection<InstituteSubscribe>();
        }
    }
}
