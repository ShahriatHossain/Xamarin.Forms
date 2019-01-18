using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using MnMLilon.Service.Configuration;

namespace MnMLilon.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                Log.Debug(TAG, "From: " + message.From);
                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                
                var messageBody = message.GetNotification().Body.ToString();

                if (messageBody.Contains("commission"))
                {
                    BatteryCommissioningSession.Current.SetIsNewMessageArrived(true);
                }
                else if (messageBody.Contains("pm/complaint"))
                {
                    TroubleTicketSession.Current.SetIsNewMessageArrived(true);
                }

                SendNotification(messageBody);
            }
            catch(Exception ex)
            {

            }
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetPriority(2)
                .SetVibrate(new long[0])
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle("Battery Message")
                .SetContentText(messageBody)
                .SetAutoCancel(true)
                .SetDefaults(NotificationDefaults.Sound)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}