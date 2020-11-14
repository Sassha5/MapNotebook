using System.Threading.Tasks;
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
            var ui = _authenticator.GetUI(Application.Context);
            ui.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(ui);
        }
    }
}
