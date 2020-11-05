using System;
using System.Threading.Tasks;
using MapNotepad.Models;

namespace MapNotepad.Services.UsersManagerService
{
    public interface IUsersManagerService
    {
        Task<int> AddUserAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task<int> GetUserIdAsync(string email, string password);
    }
}
