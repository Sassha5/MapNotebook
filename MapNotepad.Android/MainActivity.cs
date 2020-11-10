using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Prism;
using Prism.Ioc;
using Acr.UserDialogs;
using Xamarin.Auth;
using Xamarin.Forms;
using MapNotepad.Services.AuthorizationService.Twitter;
using MapNotepad.Droid.Services;
using MapNotepad.Services.NotificationService;
using Android.Content;

namespace MapNotepad.Droid
{
    [Activity(Label = "MapNotepad",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState); //Initialize GoogleMaps here
            UserDialogs.Init(this);


            //var auth = new OAuth1Authenticator(consumerKey: "j04wQhygfyVovpbJDVL5bhMJ4",
            //                                    consumerSecret: "I0MD4YLSXG3WIQR5uJah6UZBvVk5a2dohdTlwy8NZO7h43k6mF",
            //                                    requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
            //                                    authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
            //                                    accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
            //                                    callbackUrl: new Uri("http://mobile.twitter.com"));

            //auth.Completed += twitter_auth_Completed;

            //StartActivity(auth.GetUI(this));

            //var intent = new Intent(this, typeof(TwitterAuthorizationService));
            //StartService(intent);

            LoadApplication(new App(new AndroidInitializer()));
        }


        private void twitter_auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                Console.WriteLine();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ITwitterAuthorizationService, TwitterAuthorizationService>();
            containerRegistry.RegisterSingleton<INotificationService, PushNotificationService>();
        }
    }
}