using System.Threading.Tasks;
using MapNotepad.Models;
using MapNotepad.Services.UsersManagerService;

namespace MapNotepad.Services.RegistrationService
{
    class RegistrationService : IRegistrationService
    {
        private readonly IUsersManagerService _usersManagerService;

        public RegistrationService(IUsersManagerService usersManagerService)
        {
            _usersManagerService = usersManagerService;
        }

        #region -- IRegistrationService Implementation --

        public Task<int> RegisterAsync(string name, string email, string password)
        {
            User newUser = new User()
            {
                Name = name,
                Email = email,
                Password = password
            };
            return _usersManagerService.AddUserAsync(newUser); ;
        }

        public Task<bool> CheckEmailTakenAsync(string email)
        {
            return _usersManagerService.EmailExistsAsync(email);
        }

        #endregion
    }
}
