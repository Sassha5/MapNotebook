using System;
namespace MapNotepad.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        bool TryAuthorize(string email, string password);
        void Authorize(string email, string password);
        void Logout();
    }
}
