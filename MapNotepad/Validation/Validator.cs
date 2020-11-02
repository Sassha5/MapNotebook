using System;
using System.Text.RegularExpressions;
using MapNotepad.Enums;

namespace MapNotepad.Validation
{
    public class Validator
    {
        private static Regex _passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

        static Validator()
        {
        }

        public static ValidationStatus CheckNewUser(string email, string password, string confirmPassword)
        {
            ValidationStatus status;

            if (password.Equals(confirmPassword))
            {
                status = CheckEmail(email);
                if (status == ValidationStatus.Success)
                {
                    status = CheckPassword(password);
                }
            }
            else
            {
                status = ValidationStatus.PasswordsAreNotEqual;
            }

            return status;
        }

        public static ValidationStatus CheckEmail(string email)
        {                                 //этот метод должен проверять наличие имейла в базе, чтобы не было повторяющихся
            //ValidationStatus status;                           //вынести эту проверку сразу во вьюмодель?
            //if (_usersManagerService.TryFindMail(email)) { status = ValidationStatus.EmailIsTaken; }
            //else if (email.Length < Constants.MinEmailLength) { status = ValidationStatus.EmailIsTooShort; }
            //else if (char.IsDigit(email[0])) { status = ValidationStatus.EmailStartsWithNumber; }
            //else { status = ValidationStatus.Success; }
            //// TODO regex for email check
            //return status;
            return ValidationStatus.Success;
        }                                       

        public static ValidationStatus CheckPassword(string password)
        {
            ValidationStatus status;

            if (password.Length < Constants.MinPasswordLength) { status = ValidationStatus.PasswordIsTooShort; }
            else if (!_passwordRegex.IsMatch(password)) { status = ValidationStatus.PasswordIsWeak; }
            else { status = ValidationStatus.Success; }

            return status;
        }
    }
}
