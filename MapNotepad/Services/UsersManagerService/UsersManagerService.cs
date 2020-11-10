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

        #endregion
    }
}
