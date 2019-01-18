using ButtonCircle.FormsPlugin.iOS;
using FFImageLoading.Forms.Touch;
using Firebase.CloudMessaging;
using Firebase.InstanceID;
using Foundation;
using Pigeon.Services.Implementation;
using Pigeon.Services.Model;
using SuaveControls.FloatingActionButton.iOS.Renderers;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using System;
using System.Collections.ObjectModel;
using UIKit;
using UserNotifications;

namespace Pigeon.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // for circle button
            ButtonCircleRenderer.Init();

            // for FFImageLoading cache image
            CachedImageRenderer.Init();

            // for floating action button
            FloatingActionButtonRenderer.InitRenderer();

            new SfChartRenderer();
            Firebase.Core.App.Configure();
            RegisterAppForRemoteNotification();
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            Messaging.SharedInstance.ShouldEstablishDirectChannel = true;

            //Messaging.SharedInstance.ShouldEstablishDirectChannel(error =>
            //{
            //    if (error != null)
            //    {
            //        // Handle if something went wrong while connecting
            //    }
            //    else
            //    {
            //        // Let the user know that connection was successful
            //    }
            //});
#pragma warning restore CS0618 // Type or member is obsolete
            string token = GenerateFcmRegistrationToken();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

        private static void RegisterAppForRemoteNotification()
        {
            // Register your app for remote notifications.
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10 or later
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });

                //TODO - these snippets of codes will be using later on
                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

                // For iOS 10 data message (sent via FCM)
                Messaging.SharedInstance.Delegate = new CustomMessagingDelegate();
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
        }

        private static string GenerateFcmRegistrationToken()
        {
            var token = InstanceId.SharedInstance.Token;
            // Monitor token generation
            InstanceId.Notifications.ObserveTokenRefresh((sender, e) =>
            {
                // Note that this callback will be fired everytime a new token is generated, including the first
                // time. So if you need to retrieve the token as soon as it is available this is where that
                // should be done.
                var refreshedToken = InstanceId.SharedInstance.Token;

                // Do your magic to refresh the token where is needed
            });

            return token;
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
            Messaging.SharedInstance.Disconnect();
            Console.WriteLine("Disconnected from FCM");
        }

        /* TODO this code will be enable for ios 10 later*/
        // To receive notifications in foreground on iOS 10 devices.
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            // Do your magic to handle the notification data
            System.Console.WriteLine(notification.Request.Content.UserInfo);
        }

        // Receive data message on iOS 10 devices.
        public void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
        {
            Console.WriteLine(remoteMessage.AppData);
        }

        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            // Do your magic to handle the notification data
            //System.Console.WriteLine (userInfo);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.
            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);

            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);
        }

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            application.RegisterForRemoteNotifications();
            //SubscribeFCMTopic();
        }
        // For ios10 push notification subscribe
        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            SubscribeFCMTopic();
        }


        private static async void SubscribeFCMTopic()
        {
            ObservableCollection<ChannelApi> subscribeChannels = await new ChannelService().GetSubscribeChannelByUserAsync(string.Empty);
            if (subscribeChannels != null)
            {
                foreach (var channel in subscribeChannels)
                {
                    if (!channel.OwnerFlag)
                    {
                        Messaging.SharedInstance.Subscribe("/topics/" + channel.Id.ToString());
                    }
                }
            }
        }
    }
}
