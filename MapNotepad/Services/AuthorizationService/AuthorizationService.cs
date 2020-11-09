using System.Threading.Tasks;
using MapNotepad.Services.SettingsService;
using MapNotepad.Services.UsersManagerService;

namespace MapNotepad.Services.AuthorizationService
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly IUsersManagerService _usersManagerService;
        private readonly ISettingsService _settingsManagerService;

        public AuthorizationService(IUsersManagerService usersManagerService,
                                    ISettingsService settingsManagerService)
        {
            _settingsManagerService = settingsManagerService;
            _usersManagerService = usersManagerService;
        }

        public async void AuthorizeAsync(string email, string password)
        {
            var userId = await _usersManagerService.GetUserIdAsync(email, password);
            _settingsManagerService.AuthorizedUserID = userId;
        }

        public bool IsAuthorized()
        {
            return _settingsManagerService.AuthorizedUserID != Constants.NoAuthorizedUser;
        }

        public void Logout()
        {
            _settingsManagerService.ClearData();
        }

        public async Task<bool> CanAuthorizeAsync(string email, string password)
        {
            var userId = await _usersManagerService.GetUserIdAsync(email, password);
            return userId != Constants.NoAuthorizedUser;
        }
    }
}
