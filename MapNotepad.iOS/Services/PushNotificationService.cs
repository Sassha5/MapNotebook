using System;
using System.Threading.Tasks;
using Foundation;
using MapNotepad.Services.NotificationService;
using UserNotifications;

namespace MapNotepad.iOS.Services
{
    public class PushNotificationService : INotificationService
    {
        public PushNotificationService()
        {
        }

        public Task SendPush(string title, string text)
        {
            var options = new UNNotificationAttachmentOptions();  

            var content = new UNMutableNotificationContent();  
            content.Title = title;  
            content.Body = text;  
            content.Badge = 1; 
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);  

            var requestID = "sampleRequest";  
            var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);  

            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>  
            {  
                if (err != null)  
                {  
                    // Throw an error...  
                }  
            });
            return Task.FromResult(true);
        }
    }
}
