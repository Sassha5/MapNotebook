using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapNotepad.Models;
using MapNotepad.Services.RepositoryService;
using MapNotepad.Services.SettingsService;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services.PinsManagerService
{
    class PinsManagerService : IPinsManagerService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly ISettingsService _settingsManagerService;

        public PinsManagerService(IRepositoryService repositoryService,
                                    ISettingsService settingsManagerService)
        {
            _settingsManagerService = settingsManagerService;
            _repositoryService = repositoryService;
            _repositoryService.CreateTableAsync<CustomPin>();
        }

        public Position LastMapPosition
        {
            get => _settingsManagerService.LastMapPosition;
            set => _settingsManagerService.LastMapPosition = value;
        }

        #region -- IPinsManagerService Implementation --

        public async Task<int> SavePinAsync(CustomPin pin)
        {
            pin.UserId = _settingsManagerService.AuthorizedUserID;

            int id;

            if (pin.Id != 0)
            {
                await _repositoryService.UpdateItemAsync(pin);
                id = pin.Id;
            }
            else
            {
                id = await _repositoryService.InsertItemAsync(pin);
            }

            return id;
        }

        public Task<int> DeletePinAsync(CustomPin pin)
        {
            return _repositoryService.DeleteItemAsync<CustomPin>(pin.Id);
        }

        public async Task<IEnumerable<CustomPin>> GetCurrentUserPinsAsync(string searchValue)
        {
            var pins = await _repositoryService.GetItemsAsync<CustomPin>();
            var currentPins = pins.Where(x => x.UserId == _settingsManagerService.AuthorizedUserID);

            if (!string.IsNullOrEmpty(searchValue))
            {
                currentPins = currentPins.Where(x => x.Label.ToLower().Contains(searchValue.ToLower())
                          || x.Description.ToLower().Contains(searchValue.ToLower()));
            }

            return currentPins;
        }

        #endregion
    }
}
