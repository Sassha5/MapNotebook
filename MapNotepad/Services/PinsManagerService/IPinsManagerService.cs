using System;
using System.Collections.Generic;
using MapNotepad.Models;

namespace MapNotepad.Services.PinsManagerService
{
    public interface IPinsManagerService
    {
        IEnumerable<CustomPin> GetUserPins(int UserId);
        int AddPin(CustomPin pin);
    }
}
