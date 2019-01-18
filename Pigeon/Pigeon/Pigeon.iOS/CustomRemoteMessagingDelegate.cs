using System;
using Firebase.CloudMessaging;

namespace Pigeon.iOS
{
    public class CustomRemoteMessagingDelegate : MessagingDelegate
    {
        public CustomRemoteMessagingDelegate() { }
        public override void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
        {
            throw new NotImplementedException();
        }
    }
}