using System;
namespace MapNotepad.Enums
{
    public enum ValidationStatus
    {
        Success,
        EmailIsTooShort,
        EmailIsTaken,
        EmailStartsWithNumber,
        PasswordIsTooShort,
        PasswordIsWeak,
        PasswordsAreNotEqual
    }
}
