//using Tailoryfy.Common.Helper;
//using ModernHttpClient;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Plugin.FilePicker.Abstractions;
//using Plugin.Media.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace Tailoryfy.Services
//{
//    public abstract class BaseService
//    {
//        protected async Task<HttpResponseMessage> PostAsync(string action, bool isAuthorized, params KeyValuePair<string, object>[] inputs)
//        {
//            if (!HasInternetConnection())
//            {
//                return new HttpResponseMessage();
//            }

//            var uri = ApiSettings.GetApiUri(action);

//            JObject obj = new JObject();

//            foreach (var item in inputs)
//            {
//                obj.Add(item.Key, item.Value?.ToString());
//            }
//            var json = JsonConvert.SerializeObject(obj);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");

//            var client = new HttpClient(new NativeMessageHandler());
//            if (isAuthorized)
//            {
//                var accessToken = string.Empty;
//                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
//            }

//            return await client.PostAsync(uri, content);
//        }

//        protected async Task<string> PostAndReadAsStringAsync(string action, bool isAuthorized, params KeyValuePair<string, object>[] inputs)
//        {
//            var response = await PostAsync(action, isAuthorized, inputs);
//            return await response.Content.ReadAsStringAsync();
//        }

//        protected async Task<HttpResponseMessage> PostMasterChildObjAsync(string action, bool isAuthorized, object obj)
//        {
//            if (!HasInternetConnection())
//            {
//                return new HttpResponseMessage();
//            }

//            var uri = ApiSettings.GetApiUri(action);
//            var json = JsonConvert.SerializeObject(obj);
//            var t = JsonConvert.DeserializeObject(json);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");

//            var client = new HttpClient(new NativeMessageHandler());
//            if (isAuthorized)
//            {
//                var accessToken = string.Empty;
//                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
//            }

//            return await client.PostAsync(uri, content);
//        }

//        protected async Task<string> PostMasterChildObjAndReadAsStringAsync(string action, bool isAuthorized, object obj)
//        {
//            var response = await PostMasterChildObjAsync(action, isAuthorized, obj);
//            return await response.Content.ReadAsStringAsync();
//        }

//        protected async Task<HttpResponseMessage> GetAsync(string action, bool isAuthorized)
//        {
//            if (!HasInternetConnection())
//            {
//                return new HttpResponseMessage();
//            }

//            var uri = ApiSettings.GetApiUri(action);
//            var client = new HttpClient(new NativeMessageHandler());
//            if (isAuthorized)
//            {
//                var accessToken = string.Empty;
//                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
//            }

//            return await client.GetAsync(uri);
//        }

//        protected async Task<string> GetAndReadAsStringAsync(string action, bool isAuthorized)
//        {
//            var response = await GetAsync(action, isAuthorized);
//            return await response.Content.ReadAsStringAsync();
//        }

//        protected async Task<HttpResponseMessage> PutAsync(string action, bool isAuthorized, params KeyValuePair<string, object>[] inputs)
//        {
//            if (!HasInternetConnection())
//            {
//                return new HttpResponseMessage();
//            }

//            var uri = ApiSettings.GetApiUri(action);
//            JObject obj = new JObject();
//            foreach (var item in inputs)
//            {
//                obj.Add(item.Key, item.Value?.ToString());
//            }
//            var json = JsonConvert.SerializeObject(obj);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");
//            var client = new HttpClient(new NativeMessageHandler());
//            if (isAuthorized)
//            {
//                var accessToken = string.Empty;
//                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
//            }

//            return await client.PutAsync(uri, content);
//        }

//        protected async Task<string> PutAndReadAsStringAsync(string action, bool isAuthorized)
//        {
//            var response = await PutAsync(action, isAuthorized);
//            return await response.Content.ReadAsStringAsync();
//        }

//        protected async Task<string> UploadAndReadAsStringAsync(string action, bool isAuthorized, dynamic file)
//        {
//            var response = await Upload(action, isAuthorized, file);
//            return await response.Content.ReadAsStringAsync();
//        }

//        protected async Task<HttpResponseMessage> Upload(string action, bool isAuthorized, dynamic file)
//        {
//            try
//            {
//                if (!HasInternetConnection())
//                {
//                    return new HttpResponseMessage();
//                }

//                var client = new HttpClient();
//                var uri = ApiSettings.GetApiUri(action);

//                if (file != null)
//                {
//                    using (var content = new MultipartFormDataContent())
//                    {
//                        if (file is MediaFile)
//                        {
//                            content.Add(new StreamContent(ConvertByteArrayToStream(ConvertMediaFiletoByteArray(file))),
//                            "file1",
//                            "file1.png");
//                        }
//                        else if (file is FileData)
//                        {
//                            content.Add(new StreamContent(ConvertByteArrayToStream(ConvertFileDatatoByteArray(file))),
//                            "file1",
//                            "file1" + Path.GetExtension(file.FileName));
//                        }
//                        else if (file is MemoryStream)
//                        {
//                            content.Add(new StreamContent(file),
//                            "file1",
//                            "file1.png");
//                        }

//                        return await client.PostAsync(uri, content);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {

//            }
//            return new HttpResponseMessage();
//        }

//        protected KeyValuePair<string, object> CreateInput(string paramName, object value) => new KeyValuePair<string, object>(paramName, value);

//        protected bool IsSuccess(string response, bool shouldLookInComplexResponse)
//        {
//            JObject jObject = JObject.Parse(response);
//            return IsSuccess(jObject["y_json"].ToObject<JObject>());
//        }

//        protected bool IsSuccess(string response)
//        {
//            return IsSuccess(JObject.Parse(response));
//        }
//        protected bool IsSuccess(JObject jObject)
//        {
//            var successsCode = jObject["successcode"]?.ToObject<int>();
//            return successsCode == 200;
//        }

//        protected bool HasInternetConnection()
//        {
//            return CommonHelper.DoIHaveInternet();
//        }

//        //convert bytearray to stream
//        private MemoryStream ConvertByteArrayToStream(byte[] byteArray)
//        {
//            var ms = new MemoryStream();
//            ms.Write(byteArray, 0, byteArray.Length);
//            ms.Position = 0;
//            return ms;
//        }

//        private byte[] ConvertMediaFiletoByteArray(MediaFile file)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                file.GetStream().CopyTo(memoryStream);
//                //file.Dispose();
//                return memoryStream.ToArray();
//            }
//        }

//        private byte[] ConvertFileDatatoByteArray(FileData file)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                //file.GetStream().CopyTo(memoryStream);
//                //file.Dispose();
//                return memoryStream.ToArray();
//            }
//        }
//    }
//}
