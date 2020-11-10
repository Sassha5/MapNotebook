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

        public async Task AuthorizeAsync(string email, string password) //TODO return Task
        {
            var userId = await _usersManagerService.GetUserIdAsync(email, password);
            AuthorizeAsync(userId);
        }

        public void AuthorizeAsync(int id)
        {
            _settingsManagerService.AuthorizedUserID = id;
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
