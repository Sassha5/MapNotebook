using System;
using System.Collections.Generic;
using MapNotepad.Models;

namespace MapNotepad.Services.PinsManagerService
{
    public interface IPinsManagerService
    {
        IEnumerable<CustomPin> GetCurrentUserPins();
        IEnumerable<CustomPin> GetCurrentUserPins(string searchValue);
        int AddPin(CustomPin pin);
        int DeletePin(CustomPin pin);
    }
}
