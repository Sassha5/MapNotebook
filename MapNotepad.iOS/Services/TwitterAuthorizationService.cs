using System;
using System.Diagnostics;
using MapNotepad.Services.AuthorizationService.Twitter;
using UIKit;

namespace MapNotepad.iOS.Services
{
    public class TwitterAuthorizationService : TwitterAuthorizationServiceBase
    {
        public override void Login()
        {
            base.Login();

            try
            {
                var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }
                vc.PresentViewController(_authenticator.GetUI(), true, null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
