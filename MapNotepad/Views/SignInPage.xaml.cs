using Xamarin.Forms;

namespace MapNotepad.Views
{
    public partial class SignInPage : BaseContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            signInButton.IsEnabled = false;
        }

        void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(emailInput.Text) ||
                string.IsNullOrEmpty(passwordInput.Text))
            {
                signInButton.IsEnabled = false;
            }
            else
            {
                signInButton.IsEnabled = true;
            }
        }
    }
}
