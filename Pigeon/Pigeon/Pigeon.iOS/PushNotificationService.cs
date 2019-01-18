using Firebase.CloudMessaging;
using Pigeon.iOS;
using Pigeon.Services.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotificationService))]
namespace Pigeon.iOS
{
    public class PushNotificationService : IPushNotificationService
    {
        public void SubscribeChannelForNotification(string channelId)
        {
            Messaging.SharedInstance.Subscribe(channelId);
        }

        public void UnSubscribeChannelForNotification(string channelId)
        {
            Messaging.SharedInstance.Unsubscribe(channelId);
        }
    }
}