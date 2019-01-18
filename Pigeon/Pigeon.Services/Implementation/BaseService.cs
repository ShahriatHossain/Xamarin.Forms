using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pigeon.LocalDataAccess.Model;
using Pigeon.Services.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public abstract class BaseService
    {
        protected async Task<HttpResponseMessage> PostAsync(string action, bool isAuthorized, params KeyValuePair<string, object>[] inputs)
        {
            var uri = GlobalVariables.GetApiUri(action);
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
                var accessToken = LocalStorageSettings.Token;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
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
            var uri = GlobalVariables.GetApiUri(action);
            var client = new HttpClient(new NativeMessageHandler());
            if (isAuthorized)
            {
                var accessToken = LocalStorageSettings.Token;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
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
            var uri = GlobalVariables.GetApiUri(action);
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
                var accessToken = LocalStorageSettings.Token;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            return await client.PutAsync(uri, content);
        }

        protected async Task<string> PutAndReadAsStringAsync(string action, bool isAuthorized)
        {
            var response = await PutAsync(action, isAuthorized);
            return await response.Content.ReadAsStringAsync();
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

        protected async Task<string> UploadAndReadAsStringAsync(string action, bool isAuthorized, dynamic file, string fileType)
        {
            var response = await Upload(action, isAuthorized, file, fileType);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<HttpResponseMessage> Upload(string action, bool isAuthorized, dynamic file, string fileType)
        {
            try
            {
                var client = new HttpClient();
                var uri = GlobalVariables.GetApiUri(action);

                if (isAuthorized)
                {
                    var accessToken = LocalStorageSettings.Token;
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                }

                if (file != null)
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        if (file is MemoryStream)
                        {
                            content.Add(new StreamContent(file),
                            "file1",
                            "file1" + fileType);
                        }

                        return await client.PostAsync(uri, content);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new HttpResponseMessage();
        }


        //convert bytearray to stream
        public MemoryStream ConvertByteArrayToStream(byte[] byteArray)
        {
            var ms = new MemoryStream();
            ms.Write(byteArray, 0, byteArray.Length);
            ms.Position = 0;
            return ms;
        }
    }
}
