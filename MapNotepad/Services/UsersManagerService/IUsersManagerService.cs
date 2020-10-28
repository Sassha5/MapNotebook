using System;
using MapNotepad.Models;

namespace MapNotepad.Services.UsersManagerService
{
    public interface IUsersManagerService
    {
        int AddUser(User user);
        bool TryFindMail(string email);
        int GetUserId(string email, string password);
    }
}
