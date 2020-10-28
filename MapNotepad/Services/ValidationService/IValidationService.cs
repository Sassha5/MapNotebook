using System;
using MapNotepad.Enums;

namespace MapNotepad.Services.ValidationService
{
    public interface IValidationService
    {
        ValidationStatus ValidateNewUser(string email, string password, string confirmPassword);
        ValidationStatus ValidateEmail(string email);
        ValidationStatus ValidatePassword(string password);
    }
}
