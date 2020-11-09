using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using MapNotepad.Services.AuthorizationService;
using Xamarin.Auth;

namespace MapNotepad.Droid.Services
{
    [Service]
    public class TwitterAuthorizationService : Service, ITwitterAuthorizationService
    {
        private static TwitterAuthorizationService _instance;
        public static TwitterAuthorizationService Instance => _instance ??= new TwitterAuthorizationService();

        public Task Login()
        {
            //var auth2 = new OAuth2Authenticator();
            var auth = new OAuth1Authenticator(consumerKey: "j04wQhygfyVovpbJDVL5bhMJ4",
                                                consumerSecret: "I0MD4YLSXG3WIQR5uJah6UZBvVk5a2dohdTlwy8NZO7h43k6mF",
                                                requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
                                                authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
                                                accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
                                                callbackUrl: new Uri("http://mobile.twitter.com"));

            auth.Completed += twitter_auth_Completed;

            StartActivity(auth.GetUI(Application.Context));

            return Task.FromResult(true);
        }

        public override IBinder OnBind(Intent intent) => null;

        private void twitter_auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                Console.WriteLine();
            }
        }
    }
}
