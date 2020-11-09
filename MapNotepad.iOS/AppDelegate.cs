using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using MapNotepad.iOS.Services;
using MapNotepad.Services.NotificationService;
using Prism;
using Prism.Ioc;
using UIKit;
using UserNotifications;

namespace MapNotepad.iOS
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
            UNUserNotificationCenter.Current.Delegate = new iOSBannerNotification();
            var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
            UIApplication.SharedApplication.RegisterUserNotificationSettings(notificationSettings);

            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            LoadApplication(new App(new iOSInitializer()));
            Xamarin.FormsGoogleMaps.Init("AIzaSyBOj66llhOkfTyNFH1XhpjOeyS1zJy85T4");

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Ask the user for permission to get notifications on iOS 10.0+
                UNUserNotificationCenter.Current.RequestAuthorization(
                        UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                        (approved, error) => { });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                // Ask the user for permission to get notifications on iOS 8.0+
                var settings = UIUserNotificationSettings.GetSettingsForTypes(
                        UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                        new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Sound | UIUserNotificationType.Badge, null);

            uiApplication.RegisterUserNotificationSettings(notificationSettings);

            uiApplication.RegisterForRemoteNotifications();

            base.OnActivated(uiApplication);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<ITwitterAuthorizationService, TwitterAuthorizationService>();
            containerRegistry.RegisterSingleton<INotificationService, PushNotificationService>();
        }
    }

    internal class iOSBannerNotification : UNUserNotificationCenterDelegate
    {
        public iOSBannerNotification()
        {

        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert);
        }
    }
}
