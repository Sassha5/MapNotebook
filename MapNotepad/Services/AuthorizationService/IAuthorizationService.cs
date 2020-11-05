using System;
using System.Threading.Tasks;

namespace MapNotepad.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        Task<bool> CanAuthorizeAsync(string email, string password);
        void AuthorizeAsync(string email, string password);
        void Logout();
        bool IsAuthorized();
    }
}
