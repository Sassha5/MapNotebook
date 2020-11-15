using System;
using System.Diagnostics;
using Android.App;
using Android.Content;
using MapNotepad.Services.AuthorizationService.Twitter;

namespace MapNotepad.Droid.Services
{
    public class TwitterAuthorizationService : TwitterAuthorizationServiceBase
    {
        public override void Login()
        {
            base.Login();

            try
            {
                var ui = _authenticator.GetUI(Application.Context);
                ui.AddFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(ui);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
