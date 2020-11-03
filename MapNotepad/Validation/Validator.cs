using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MapNotepad.Validation
{
    public class Validator
    {
        private static Regex _passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

        static Validator()
        {
        }

        public static bool CheckEmail(string email) //do i need validation status, or just bool?
        {
            bool result;
            try
            {
                MailAddress mail = new MailAddress(email);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }                                       

        public static bool CheckPassword(string password)
        {
            return _passwordRegex.IsMatch(password);
        }
    }
}
