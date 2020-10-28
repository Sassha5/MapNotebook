using System;
using System.Text.RegularExpressions;
using MapNotepad.Enums;
using MapNotepad.Services.UsersManagerService;

namespace MapNotepad.Services.ValidationService
{
    public class ValidationService : IValidationService
    {
        private readonly IUsersManagerService _usersManagerService;

        public ValidationService(IUsersManagerService usersManagerService)
        {
            _usersManagerService = usersManagerService;
        }

        public ValidationStatus ValidateNewUser(string email, string password, string confirmPassword)
        {
            ValidationStatus status;

            if (password.Equals(confirmPassword))
            {
                status = ValidateEmail(email);
                if (status == ValidationStatus.Success)
                {
                    status = ValidatePassword(password);
                }
            }
            else
            {
                status = ValidationStatus.PasswordsAreNotEqual;
            }

            return status;
        }

        public ValidationStatus ValidateEmail(string email)
        {
            ValidationStatus status;
            if (_usersManagerService.TryFindMail(email)) { status = ValidationStatus.EmailIsTaken; }
            else if (email.Length < Constants.MinEmailLength) { status = ValidationStatus.EmailIsTooShort; }
            else if (char.IsDigit(email[0])) { status = ValidationStatus.EmailStartsWithNumber; }
            else { status = ValidationStatus.Success; }
            // TODO regex for email check
            return status;
        }

        public ValidationStatus ValidatePassword(string password)
        {
            ValidationStatus status;

            if (password.Length < Constants.MinPasswordLength) { status = ValidationStatus.PasswordIsTooShort; }
            else if (!PasswordRegexMatch(password)) { status = ValidationStatus.PasswordIsWeak; }
            else { status = ValidationStatus.Success; }

            return status;
        }

        private bool PasswordRegexMatch(string pass)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            return hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasLowerChar.IsMatch(pass);
        }
    }
}
