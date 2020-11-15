using Foundation;
using MapNotepad.iOS.Services;
using MapNotepad.Services.AuthorizationService.Twitter;
using MapNotepad.Services.PermissionService;
using Plugin.FacebookClient;
using Prism;
using Prism.Ioc;
using UIKit;

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
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App(new iOSInitializer()));

            Xamarin.FormsGoogleMaps.Init("AIzaSyBOj66llhOkfTyNFH1XhpjOeyS1zJy85T4");

            FacebookClientManager.Initialize(app, options);

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            FacebookClientManager.OnActivated();

            base.OnActivated(uiApplication);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return FacebookClientManager.OpenUrl(app, url, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ITwitterAuthorizationService, TwitterAuthorizationService>();
            containerRegistry.RegisterSingleton<IPermissionService, PermissionService>();
        }
    }
}
