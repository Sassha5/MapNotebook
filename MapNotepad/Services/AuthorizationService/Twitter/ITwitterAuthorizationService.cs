using System;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace MapNotepad.Services.AuthorizationService.Twitter
{
    public interface ITwitterAuthorizationService
    {
        Task Login();
        void RegisterAuthDelegate(IAuthDelegate authDelegate);
    }
}
