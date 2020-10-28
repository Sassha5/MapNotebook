using System;
using System.Collections.Generic;
using System.Linq;
using MapNotepad.Models;
using MapNotepad.Services.RepositoryService;
using MapNotepad.Services.SettingsManagerService;

namespace MapNotepad.Services.PinsManagerService
{
    class PinsManagerService : IPinsManagerService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly ISettingsManagerService _settingsManagerService;

        public PinsManagerService(IRepositoryService repositoryService,
                                    ISettingsManagerService settingsManagerService)
        {
            _settingsManagerService = settingsManagerService;
            _repositoryService = repositoryService;
            _repositoryService.CreateTable<CustomPin>();
        }

        public int AddPin(CustomPin pin)
        {
            pin.UserId = _settingsManagerService.AuthorizedUserID;
            return _repositoryService.InsertItem(pin);
        }

        public IEnumerable<CustomPin> GetCurrentUserPins()
        {
            return _repositoryService.GetItems<CustomPin>().Where(x => x.UserId == _settingsManagerService.AuthorizedUserID);
        }
    }
}
