using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services.RegistrationService;
using MapNotepad.Validation;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {

        private readonly IRegistrationService _registrationService;
        private readonly IUserDialogs _userDialogs;

        public SignUpPageViewModel(INavigationService navigationService,
                                    IRegistrationService registrationService,
                                    IUserDialogs userDialogs)
                                    : base(navigationService)
        {
            _userDialogs = userDialogs;
            _registrationService = registrationService;
        }

        #region -- Public Properties --

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(User), out User user))
            {
                Name = user.Name;
                Email = user.Email;
            }
            else
            {
                Name = string.Empty;
            }
        }

        #endregion

        #region -- Commands --

        private ICommand _RegisterCommand;
        public ICommand RegisterCommand => _RegisterCommand ??= new Command(OnRegisterCommandAsync);

        #endregion

        #region -- Command Methods --

        private async void OnRegisterCommandAsync()
        {
            switch (await ValidateAsync())
            {
                case (int)Status.FieldsEmpty:
                    await _userDialogs.AlertAsync(Resources["FieldsEmpty"], Resources["Oops"], Resources["Damn"]); break;
                case (int)Status.EmailIsTaken:
                    await _userDialogs.AlertAsync(Resources["EmailIsTaken"], Resources["Oops"], Resources["Damn"]); break;
                case (int)Status.BadEmail:
                    await _userDialogs.AlertAsync(Resources["BadEmail"], Resources["Oops"], Resources["Damn"]); break;
                case (int)Status.BadPassword:
                    await _userDialogs.AlertAsync(Resources["BadPassword"], Resources["Oops"], Resources["Damn"]); break;
                case (int)Status.PasswordsNotEqual:
                    await _userDialogs.AlertAsync(Resources["PasswordsAreNotEqual"], Resources["Oops"], Resources["Damn"]); break;
                case (int)Status.Success:
                    await _userDialogs.AlertAsync(Resources["RedirectingToSignIn"], Resources["Success"], Resources["Finally"]);
                    ValidationSuccess();
                    break;
                default:
                    await _userDialogs.AlertAsync(Resources["Unknown"], Resources["Oops"], Resources["Damn"]); break;
            }
        }

        #endregion

        #region -- Private Helpers --

        private enum Status
        {
            FieldsEmpty,
            EmailIsTaken,
            BadEmail,
            BadPassword,
            PasswordsNotEqual,
            Success
        }

        private async Task<int> ValidateAsync()
        {
            Status status;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password)) { status = Status.FieldsEmpty; }
            else if (!Validator.CheckEmail(Email)) { status = Status.BadEmail; }
            else if (await _registrationService.CheckEmailTakenAsync(Email)) { status = Status.EmailIsTaken; }
            else if (!Validator.CheckPassword(Password)) { status = Status.BadPassword; }
            else if (!Password.Equals(ConfirmPassword)) { status = Status.PasswordsNotEqual; }
            else { status = Status.Success; }

            return (int)status;
        }

        private async void ValidationSuccess()
        {
            await _registrationService.RegisterAsync(Name, Email, Password);
            NavigationParameters navParams = new NavigationParameters
            {
                { $"{nameof(Email)}", Email }
            };
            await NavigationService.GoBackAsync(navParams);
        }

        #endregion
    }
}
