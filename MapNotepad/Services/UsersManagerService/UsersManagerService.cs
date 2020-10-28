using System;
using System.Linq;
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
            _repositoryService.CreateTable<User>();
        }

        public int AddUser(User user)
        {
            return _repositoryService.InsertItem(user);
        }

        public int GetUserId(string email, string password)
        {
            int id;
            var user = _repositoryService.GetItems<User>().FirstOrDefault(x => x.Email == email && x.Password == password);
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

        public bool TryFindMail(string email)
        {
            var user = _repositoryService.GetItems<User>().Where(x => x.Email == email);
            return user != null;
        }
    }
}
