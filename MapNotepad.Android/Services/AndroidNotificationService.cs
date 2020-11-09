using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;

namespace MapNotepad.Droid.Services
{
    [Service(Name = "sassha5.mapnotepad.AndroidNotificationService", Label = "Notification")]
    [IntentFilter(new string[] { SEND_PUSH_INTENT })]
    public class AndroidNotificationService : IntentService
    {
        public const string SEND_PUSH_INTENT = "PleaseWork";

        protected override void OnHandleIntent(Intent intent)
        {
            if (intent.Action == SEND_PUSH_INTENT &&
                intent.Extras.ContainsKey("Title") &&
                intent.Extras.ContainsKey("Content"))
            {
                var extra = intent.Extras;
                SendPush(extra.GetString("Title"), extra.GetString("Content"));
            }
        }

        private void SendPush(string title, string content)
        {
            NotificationManager notificationManager = GetSystemService(NotificationService) as NotificationManager;

            var notificationChannel = new NotificationChannel(Application.Context.PackageName,
                                                              Application.Context.PackageName,
                                                              NotificationImportance.Max);
            notificationChannel.EnableLights(true);
            notificationChannel.EnableVibration(true);
            notificationManager.CreateNotificationChannel(notificationChannel);

            //var intent = new Intent(this, typeof(MainActivity));
            //intent.AddFlags(ActivityFlags.SingleTop);
            //intent.PutExtra(Constants.PUSH_NAVIGATE_KEY, dataValue);


            var builder = new NotificationCompat.Builder(this, Application.Context.PackageName)
                .SetContentTitle(title)
                .SetContentText(content)
                .SetSmallIcon(Resource.Drawable.notification_icon_background);

            var notification = builder.Build();

            notificationManager.Notify(0, notification);
        }
    }
}
