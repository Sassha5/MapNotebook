using System;
namespace MapNotepad.Services.RegistrationService
{
    public interface IRegistrationService
    {
        int Register(string name, string email, string password);
    }
}
