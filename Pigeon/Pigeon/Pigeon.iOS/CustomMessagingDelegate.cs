using Firebase.CloudMessaging;
using System;

namespace Pigeon.iOS
{
    public class CustomMessagingDelegate : MessagingDelegate
    {
#pragma warning disable CS0672 // Member overrides obsolete member
        public override void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
#pragma warning restore CS0672 // Member overrides obsolete member
        {
            throw new NotImplementedException();
        }

        public override void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        {
            throw new NotImplementedException();
        }
    }
}