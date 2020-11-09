using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using MapNotepad.Services.NotificationService;

namespace MapNotepad.Droid.Services
{
    public class PushNotificationService : INotificationService
    {
        #region -- INotificationService implementation --

        public Task SendPush(string title, string content)
        {
            var intent = new Intent(Application.Context, typeof(AndroidNotificationService));
            intent.SetAction(AndroidNotificationService.SEND_PUSH_INTENT);
            intent.PutExtra("Title", title);
            intent.PutExtra("Content", content);
            Application.Context.StartService(intent);
            return Task.FromResult(true);
        }

        #endregion
    }
}
