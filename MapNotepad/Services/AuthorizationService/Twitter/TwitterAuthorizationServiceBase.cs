using System;
using System.Diagnostics;
using MapNotepad.Models;
using Xamarin.Auth;

namespace MapNotepad.Services.AuthorizationService.Twitter
{
    public class TwitterAuthorizationServiceBase : ITwitterAuthorizationService
    {
        protected IAuthDelegate _authDelegate;
        protected OAuth1Authenticator _authenticator;

        public TwitterAuthorizationServiceBase()
        {
            _authenticator = new OAuth1Authenticator(consumerKey: Constants.TwitterConsumerKey,
                                                consumerSecret: Constants.TwiterSecretKey,
                                                requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
                                                authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
                                                accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
                                                callbackUrl: new Uri("https://mobile.twitter.com"));
        }

        #region -- ITwitterAuthorizationService Implementation --

        public virtual void Login()
        {
            _authenticator.Completed += twitter_auth_Completed;
        }

        public void RegisterAuthDelegate(IAuthDelegate authDelegate)
        {
            _authDelegate = authDelegate;
        }

        #endregion

        #region -- Private Helpers --

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

        #endregion
    }
}
