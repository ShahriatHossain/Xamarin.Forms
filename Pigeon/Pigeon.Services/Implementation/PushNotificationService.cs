using Newtonsoft.Json.Linq;
using Pigeon.Services.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public class PushNotificationService
    {
        public event EventHandler<PushNotificationEventArgs> SubscribeChannel, UnsubscribeChannel;

        public void Publish(int? channelId, string channelName, string notice)
        {
            this.PushToFcm(channelId, channelName, notice);
        }

        private const string FCM_API_KEY = "AIzaSyCI3IjP7U7Vqo3zPzVbDhNEGXHrnbT5oNk";
        private const string FCM_POST_URL = "https://fcm.googleapis.com/fcm/send";

        private void PushToFcm(int? channelId, string channelName, string notice)
        {
            notice = "New notice from channel: " + channelName;
            var jGcmData = new JObject();
            var jData = new JObject();
            var jCustomData = new JObject();

            jCustomData.Add("channelId", channelId.ToString());

            jData.Add("body", notice);
            jData.Add("sound", "default");
            jGcmData.Add("to", "/topics/" + channelId.ToString());
            jGcmData.Add("priority", "high");
            jGcmData.Add("notification", jData);
            jGcmData.Add("data", jCustomData);

            var url = new Uri(FCM_POST_URL);
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + FCM_API_KEY);

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

        public void Subscribe(ChannelApi channel)
        {
            if (this.SubscribeChannel != null)
            {
                this.SubscribeChannel(this, new PushNotificationEventArgs(channel));
            }
        }

        public void Unsubscribe(ChannelApi channel)
        {
            if (this.UnsubscribeChannel != null)
            {
                this.UnsubscribeChannel(this, new PushNotificationEventArgs(channel));
            }
        }
    }

    public class PushNotificationEventArgs : EventArgs
    {
        public PushNotificationEventArgs(ChannelApi channel)
        {
            this.Channel = channel;
        }
        public ChannelApi Channel { get; }
    }
}
