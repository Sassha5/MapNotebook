using System;
using System.Threading.Tasks;

namespace MapNotepad.Services.NotificationService
{
    public interface INotificationService
    {
        Task SendPush(string title, string text);
    }
}
