using System;
using MapNotepad.Models;

namespace MapNotepad.Services.AuthorizationService.Twitter
{
    public interface IAuthDelegate
    {
        void AuthSuccess(AuthResult result);
        void AuthFailure();
    }
}
