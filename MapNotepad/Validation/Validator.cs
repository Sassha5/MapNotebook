using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MapNotepad.Validation
{
    public class Validator
    {
        private static Regex _passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
        private static Regex _emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        static Validator()
        {
        }

        public static bool CheckEmail(string email)
        {
            return true;//_emailRegex.IsMatch(email);
        }                                       

        public static bool CheckPassword(string password)
        {
            return true;// _passwordRegex.IsMatch(password);
        }
    }
}
