using System;
using MapNotepad.Services.SettingsManagerService;
using MapNotepad.Services.UsersManagerService;

namespace MapNotepad.Services.AuthorizationService
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly IUsersManagerService _usersManagerService;
        private readonly ISettingsManagerService _settingsManagerService;

        public AuthorizationService(IUsersManagerService usersManagerService,
                                    ISettingsManagerService settingsManagerService)
        {
            _settingsManagerService = settingsManagerService;
            _usersManagerService = usersManagerService;
        }

        public void Authorize(string email, string password)
        {
            _settingsManagerService.AuthorizedUserID = _usersManagerService.GetUserId(email, password);
        }

        public void Logout()
        {
            _settingsManagerService.AuthorizedUserID = Constants.NoAuthorizedUser;
        }

        public bool TryAuthorize(string email, string password)
        {
            return !(_usersManagerService.GetUserId(email, password) == Constants.NoAuthorizedUser);
        }


    }
}
