using System;
using System.Collections.Generic;
using MapNotepad.Models;
using MapNotepad.Services.RepositoryService;

namespace MapNotepad.Services.PinsManagerService
{
    public class PinsManagerService : IPinsManagerService
    {
        private readonly IRepositoryService _repositoryService;

        public PinsManagerService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
            _repositoryService.CreateTable<CustomPin>();
        }

        public int AddPin(CustomPin pin)
        {
            return _repositoryService.InsertItem(pin);
        }

        public IEnumerable<CustomPin> GetUserPins(int userId)
        {
            return _repositoryService.GetItems<CustomPin>();//where x.userId == userId
        }
    }
}
