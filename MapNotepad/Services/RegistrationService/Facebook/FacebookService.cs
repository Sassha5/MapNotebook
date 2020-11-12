using System;
using System.Diagnostics;
using MapNotepad.Models;
using MapNotepad.Services.AuthorizationService.Twitter;
using Newtonsoft.Json;
using Plugin.FacebookClient;

namespace MapNotepad.Services.RegistrationService.Facebook
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;
        private IAuthDelegate _authDelegate;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public void RegisterAuthDelegate(IAuthDelegate authDelegate)
        {
            _authDelegate = authDelegate;
        }

        public async void TryGetUserInfoAsync()
        {
            try
            {
                if (_facebookClient.IsLoggedIn)
                {
                    _facebookClient.Logout();
                }

                _facebookClient.OnUserData += FacebookAuthCompleted;

                string[] fbRequestFields = { "email", "first_name", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                var result = await _facebookClient.RequestUserDataAsync(fbRequestFields, fbPermisions);

                var a = result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void FacebookAuthCompleted(object sender, FBEventArgs<string> args)
        {
            if (args != null)
            {

                if (args.Status == FacebookActionStatus.Completed)
                {
                    FacebookProfile facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(args.Data);

                    AuthResult result = new AuthResult
                    {
                        Email = facebookProfile.Email,
                        Username = facebookProfile.FirstName + " " + facebookProfile.LastName
                    };
                    _authDelegate.AuthSuccess(result);
                }
                else
                {
                    _authDelegate.AuthFailure();
                }

                _facebookClient.OnUserData -= FacebookAuthCompleted;
            }
        }

        public class FacebookProfile
        {
            public string Email { get; set; }
            public string Id { get; set; }

            [JsonProperty("last_name")]
            public string LastName { get; set; }
            [JsonProperty("first_name")]
            public string FirstName { get; set; }
        }
    }
}
