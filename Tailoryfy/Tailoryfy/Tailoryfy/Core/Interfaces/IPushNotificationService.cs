namespace Tailoryfy.Core.Repositories
{
    public interface IPushNotificationService
    {
        void SubscribeTopicForNotification(string topicId);
        void UnSubscribeTopicForNotification(string topicId);
    }
}
