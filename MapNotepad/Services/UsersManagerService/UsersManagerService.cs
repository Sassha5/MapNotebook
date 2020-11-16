using System;
using System.Linq;
using System.Threading.Tasks;
using MapNotepad.Models;
using MapNotepad.Services.RepositoryService;

namespace MapNotepad.Services.UsersManagerService
{
    class UsersManagerService : IUsersManagerService
    {
        private readonly IRepositoryService _repositoryService;

        public UsersManagerService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
            _repositoryService.CreateTableAsync<User>();
        }

        #region -- IUsersManagerService Implementation --

        public Task<int> AddUserAsync(User user)
        {
            return _repositoryService.InsertItemAsync(user);
        }

        public async Task<string> GetUserIdAsync(string email, string password)
        {
            var userEnumerable = await _repositoryService.GetItemsAsync<User>();
            var user = userEnumerable.FirstOrDefault(x => x.Email == email && x.Password == password);

            return user == null ? Constants.NoAuthorizedUser : user.Id.ToString();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            User user = (await _repositoryService.GetItemsAsync<User>()).Where(x => x.Email == email).FirstOrDefault();
            return user != null;
        }

        #endregion
    }
}
