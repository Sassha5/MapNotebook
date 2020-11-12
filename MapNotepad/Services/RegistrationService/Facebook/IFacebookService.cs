using System;
using System.Threading.Tasks;
using MapNotepad.Models;
using MapNotepad.Services.AuthorizationService.Twitter;

namespace MapNotepad.Services.RegistrationService.Facebook
{
    public interface IFacebookService
    {
        void TryGetUserInfoAsync();
        void RegisterAuthDelegate(IAuthDelegate authDelegate);
    }
}
