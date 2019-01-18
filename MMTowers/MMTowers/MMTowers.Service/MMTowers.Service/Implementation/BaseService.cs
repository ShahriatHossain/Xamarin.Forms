using MMTowers.Service.Common;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MMTowers.Service.Implementation
{
    public abstract class BaseService
    {
        protected async Task<HttpResponseMessage> PostAsync(string action, bool isAuthorized, params KeyValuePair<string, object>[] inputs)
        {
            var uri = Global.GetApiUri(action);
            JObject obj = new JObject();
            foreach (var item in inputs)
            {
                obj.Add(item.Key, item.Value?.ToString());
            }
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient(new NativeMessageHandler());
            if (isAuthorized)
            {
                var accessToken = string.Empty;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }

            if (!HasInternetConnection())
            {
                return new HttpResponseMessage();
            }

            return await client.PostAsync(uri, content);
        }

        protected async Task<string> PostAndReadAsStringAsync(string action, bool isAuthorized, params KeyValuePair<string, object>[] inputs)
        {
            var response = await PostAsync(action, isAuthorized, inputs);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<HttpResponseMessage> GetAsync(string action, bool isAuthorized)
        {
            var uri = Global.GetApiUri(action);
            var client = new HttpClient(new NativeMessageHandler());
            if (isAuthorized)
            {
                var accessToken = string.Empty;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }

            if (!HasInternetConnection())
            {
                return new HttpResponseMessage();
            }

            return await client.GetAsync(uri);
        }

        protected async Task<string> GetAndReadAsStringAsync(string action, bool isAuthorized)
        {
            var response = await GetAsync(action, isAuthorized);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<HttpResponseMessage> PutAsync(string action, bool isAuthorized, params KeyValuePair<string, object>[] inputs)
        {
            var uri = Global.GetApiUri(action);
            JObject obj = new JObject();
            foreach (var item in inputs)
            {
                obj.Add(item.Key, item.Value?.ToString());
            }
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(new NativeMessageHandler());
            if (isAuthorized)
            {
                var accessToken = string.Empty;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }

            if (!HasInternetConnection())
            {
                return new HttpResponseMessage();
            }

            return await client.PutAsync(uri, content);
        }

        protected async Task<string> PutAndReadAsStringAsync(string action, bool isAuthorized)
        {
            var response = await PutAsync(action, isAuthorized);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> UploadAndReadAsStringAsync(string action, bool isAuthorized, MediaFile mediaFile, MediaFile mediaFile2, MediaFile mediaFile3, MediaFile mediaFile4)
        {
            var response = await Upload(action, isAuthorized, mediaFile, mediaFile2, mediaFile3, mediaFile4);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<HttpResponseMessage> Upload(string action, bool isAuthorized, MediaFile mediaFile, MediaFile mediaFile2, MediaFile mediaFile3, MediaFile mediaFile4)
        {
            try
            {
                var client = new HttpClient();
                var uri = Global.GetApiUri(action);

                using (var content = new MultipartFormDataContent())
                {
                    if (mediaFile != null)
                    {
                        content.Add(new StreamContent(mediaFile.GetStream()),
                        "file1",
                        mediaFile.Path);
                    }
                    if (mediaFile2 != null)
                    {
                        content.Add(new StreamContent(mediaFile2.GetStream()),
                        "file2",
                        mediaFile2.Path);
                    }
                    if (mediaFile3 != null)
                    {
                        content.Add(new StreamContent(mediaFile3.GetStream()),
                        "file3",
                        mediaFile3.Path);
                    }
                    if (mediaFile4 != null)
                    {
                        content.Add(new StreamContent(mediaFile4.GetStream()),
                        "file4",
                        mediaFile4.Path);
                    }

                    if (!HasInternetConnection())
                    {
                        return new HttpResponseMessage();
                    }

                    return await client.PostAsync(uri, content);
                }
            }
            catch (Exception ex)
            {

            }
            return new HttpResponseMessage();
        }

        protected KeyValuePair<string, object> CreateInput(string paramName, object value) => new KeyValuePair<string, object>(paramName, value);

        protected bool IsSuccess(string response, bool shouldLookInComplexResponse)
        {
            JObject jObject = JObject.Parse(response);
            return IsSuccess(jObject["y_json"].ToObject<JObject>());
        }

        protected bool IsSuccess(string response)
        {
            return IsSuccess(JObject.Parse(response));
        }
        protected bool IsSuccess(JObject jObject)
        {
            var successsCode = jObject["successcode"]?.ToObject<int>();
            return successsCode == 200;
        }

        protected bool HasInternetConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
