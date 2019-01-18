namespace Pigeon.Services.Interface
{
    public interface IPushNotificationService
    {
        void SubscribeChannelForNotification(string channelId);
        void UnSubscribeChannelForNotification(string channelId);
    }
}
