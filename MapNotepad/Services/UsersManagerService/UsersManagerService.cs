using System;
using System.Linq;
using System.Threading.Tasks;
using MapNotepad.Models;
using MapNotepad.Services.RepositoryService;
using MapNotepad.Services.SettingsService;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services.UsersManagerService
{
    class UsersManagerService : IUsersManagerService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly ISettingsService _settingsManagerService; 

        public UsersManagerService(IRepositoryService repositoryService,
                                   ISettingsService settingsManagerService)
        {
            _repositoryService = repositoryService;
            _settingsManagerService = settingsManagerService;
            _repositoryService.CreateTableAsync<User>();
        }

        #region Methods
        public Task<int> AddUserAsync(User user)
        {
            return _repositoryService.InsertItemAsync(user);
        }

        public async Task<int> GetUserIdAsync(string email, string password)
        {
            int id;
            var userEnumerable = await _repositoryService.GetItemsAsync<User>();
            var user = userEnumerable.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                id = user.Id;
            }
            else
            {
                id = Constants.NoAuthorizedUser;
            }
            return id;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var userEnumerable = await _repositoryService.GetItemsAsync<User>();
            var user = userEnumerable.Where(x => x.Email == email);
            return user != null;
        }

        public async Task<int> SaveLastMapPositionAsync(Position position)
        {
            User user = await _repositoryService.GetItemAsync<User>(_settingsManagerService.AuthorizedUserID);
            user.LastMapPositionX = position.Latitude;
            user.LastMapPositionY = position.Longitude;
            return await _repositoryService.UpdateItemAsync(user);
        } //TODO do something

        #endregion
    }
}
