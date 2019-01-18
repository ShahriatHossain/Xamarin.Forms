namespace MnMLilon.Service.Interface
{
    public interface IPushNotificationService
    {
        void SubscribeTopicForNotification(string topicId);
        void UnSubscribeTopicForNotification(string topicId);
    }
}
