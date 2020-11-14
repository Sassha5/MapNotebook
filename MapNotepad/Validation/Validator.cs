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

        #region -- Public Static Methods --

        public static bool CheckEmail(string email)
        {
            return _emailRegex.IsMatch(email);
        }                                       

        public static bool CheckPassword(string password)
        {
            return _passwordRegex.IsMatch(password);
        }

        public static bool CheckLatitude(double latitude)
        {
            return (latitude <= 90) && (latitude >= -90);
        }

        public static bool CheckLongitude(double longitude)
        {
            return (longitude <= 180) && (longitude >= -180);
        }

        #endregion
    }
}
