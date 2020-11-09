using System;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace MapNotepad.Services.AuthorizationService
{
    public interface ITwitterAuthorizationService
    {
        Task Login();
    }
}
