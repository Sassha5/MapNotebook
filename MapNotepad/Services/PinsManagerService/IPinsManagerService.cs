using System.Collections.Generic;
using System.Threading.Tasks;
using MapNotepad.Models;

namespace MapNotepad.Services.PinsManagerService
{
    public interface IPinsManagerService
    {
        Task<IEnumerable<CustomPin>> GetCurrentUserPinsAsync(string searchValue);
        Task<int> SavePinAsync(CustomPin pin);
        Task<int> DeletePinAsync(CustomPin pin);
    }
}
