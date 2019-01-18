using MnMLilon.Droid;
using MnMLilon.Service.Interface;
using Firebase.Messaging;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotificationService))]
namespace MnMLilon.Droid
{
    public class PushNotificationService : IPushNotificationService
    {
        public void SubscribeTopicForNotification(string topicId)
        {
            try
            {
                FirebaseMessaging.Instance.SubscribeToTopic(topicId);
            }
            catch(Exception ex)
            {

            }
        }

        public void UnSubscribeTopicForNotification(string topicId)
        {
            try
            {
                FirebaseMessaging.Instance.UnsubscribeFromTopic(topicId);
            }
            catch(Exception ex)
            {

            }
        }
    }
}