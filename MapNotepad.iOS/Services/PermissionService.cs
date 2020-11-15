using System;
using System.Threading.Tasks;
using Foundation;
using MapNotepad.Services.PermissionService;
using UIKit;
using Xamarin.Essentials;

namespace MapNotepad.iOS.Services
{
    public class PermissionService : IPermissionService
    {
        #region -- IPermisstionService Implementation --

        public Task<PermissionStatus> CheckPermissionAsync<T>() where T : Permissions.BasePermission, new()
        {
            return Permissions.CheckStatusAsync<T>();
        }

        public async Task<PermissionStatus> RequestPermissionAsync<T>() where T : Permissions.BasePermission, new()
        {
            var status = await CheckPermissionAsync<T>();
            if (status == PermissionStatus.Unknown)
            {
                status = await Permissions.RequestAsync<T>();
            }

            if (status == PermissionStatus.Denied)
            {
                var okCancelAlertController = UIAlertController.Create("Permission was denied", "Would you like to change it in settings?", UIAlertControllerStyle.Alert);

                okCancelAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, alert => UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"))));
                okCancelAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, alert => Console.WriteLine("Cancel was clicked")));

                var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                vc.PresentViewController(okCancelAlertController, true, null);
            }

            return status;
        }

        #endregion
    }
}
