using Firebase.Messaging;
using Pigeon.Droid;
using Pigeon.Services;
using Pigeon.Services.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotificationService))]
namespace Pigeon.Droid
{
    public class PushNotificationService : IPushNotificationService
    {
        public void SubscribeChannelForNotification(string channelId)
        {
            FirebaseMessaging.Instance.SubscribeToTopic(channelId);
        }

        public void UnSubscribeChannelForNotification(string channelId)
        {
            FirebaseMessaging.Instance.UnsubscribeFromTopic(channelId);
        }
    }
}