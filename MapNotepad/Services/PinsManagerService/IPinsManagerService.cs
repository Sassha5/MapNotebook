using System.Collections.Generic;
using System.Threading.Tasks;
using MapNotepad.Models;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services.PinsManagerService
{
    public interface IPinsManagerService
    {
        Task<IEnumerable<CustomPin>> GetCurrentUserPinsAsync(string searchValue);
        Task<int> SavePinAsync(CustomPin pin);
        Task<int> DeletePinAsync(CustomPin pin);
        Position LastMapPosition { get; set; }
    }
}
