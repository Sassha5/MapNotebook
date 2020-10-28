using System;
using System.Collections.Generic;
using MapNotepad.Models;

namespace MapNotepad.Services.PinsManagerService
{
    public interface IPinsManagerService
    {
        IEnumerable<CustomPin> GetCurrentUserPins();
        int AddPin(CustomPin pin);
    }
}
