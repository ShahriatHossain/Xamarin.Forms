using ModernHttpClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Implementation
{
    public class PushNotificationSender
    {
        private const string GCM_API_KEY = "AIzaSyAF9H0XQ4CWS_KiHF7-O1sd8mRyvNcPCOc";
        private const string GCM_POST_URL = "https://fcm.googleapis.com/fcm/send";

        public void PushToGcm(string topicId, string topicName)
        {
            var jGcmData = new JObject();
            var jData = new JObject();
            var notice = "New notice from channel: " + topicName;
            jData.Add("message", notice);
            jData.Add("args", topicId);
            jGcmData.Add("to", "/topics/" + topicId);
            jGcmData.Add("data", jData);
            var url = new Uri(GCM_POST_URL);
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + GCM_API_KEY);

                    Task.WaitAll(client.PostAsync(url,
                        new StringContent(jGcmData.ToString(), Encoding.UTF8, "application/json"))
                            .ContinueWith(response =>
                            {
                                //
                            }));
                }
            }
            catch (Exception e)
            {
                //
            }
        }
    }
}
