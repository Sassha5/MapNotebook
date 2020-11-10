﻿using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using MapNotepad.Models;
using MapNotepad.Services.AuthorizationService.Twitter;
using Xamarin.Auth;

namespace MapNotepad.Droid.Services
{
    public class TwitterAuthorizationService : ITwitterAuthorizationService
    {
        private static TwitterAuthorizationService _instance;
        public static TwitterAuthorizationService Instance => _instance ??= new TwitterAuthorizationService();

        private IAuthDelegate _authDelegate;

        public Task Login()
        {
            var auth = new OAuth1Authenticator(consumerKey: "j04wQhygfyVovpbJDVL5bhMJ4",
                                                consumerSecret: "I0MD4YLSXG3WIQR5uJah6UZBvVk5a2dohdTlwy8NZO7h43k6mF",
                                                requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
                                                authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
                                                accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
                                                callbackUrl: new Uri("http://mobile.twitter.com"));

            auth.Completed += twitter_auth_Completed;

            var ui = auth.GetUI(Application.Context);
            ui.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(ui);

            return Task.FromResult(true);
        }

        public void RegisterAuthDelegate(IAuthDelegate authDelegate)
        {
            _authDelegate = authDelegate;
        }

        private void twitter_auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                if (eventArgs.Account.Properties.TryGetValue("user_id", out var id))
                {
                    string username;
                    eventArgs.Account.Properties.TryGetValue("screen_name", out username);
                    AuthResult result = new AuthResult()
                    {
                        Id = int.Parse(id),
                        Username = username
                    };
                    
                    _authDelegate?.AuthSuccess(result);
                }
            }
            else
            {
                _authDelegate?.AuthFailure();
            }
        }
    }
}
