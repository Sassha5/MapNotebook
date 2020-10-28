using System;
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

        public int Register(string name, string email, string password)
        {
            return _usersManagerService.AddUser(new User()
            {
                Name = name,
                Email = email,
                Password = password
            });
        }
    }
}
