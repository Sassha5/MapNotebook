using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Services.RegistrationService;
using MapNotepad.Validation;
using MapNotepad.Views;
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

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        private ICommand _RegisterCommand;
        public ICommand RegisterCommand => _RegisterCommand ??= new Command(OnRegisterCommandAsync);

        private async void OnRegisterCommandAsync()
        {
            Validator.CheckEmail(Email);
            Validator.CheckPassword(Password);
            if (!Password.Equals(ConfirmPassword))
            {

            }
            //switch (Validator.CheckNewUser(Email, Password, ConfirmPassword))
            //{
            //    case ValidationStatus.EmailIsTaken:
            //        await _userDialogs.AlertAsync(Resources["EmailIsTaken"], Resources["Oops"], Resources["Damn"]); break;
            //    case ValidationStatus.EmailIsTooShort:
            //        await _userDialogs.AlertAsync(Resources["EmailIsTooShort"], Resources["Oops"], Resources["Ok"]); break;
            //    case ValidationStatus.EmailStartsWithNumber:
            //        await _userDialogs.AlertAsync(Resources["EmailStartsWithNumber"], Resources["Oops"], Resources["Damn"]); break;
            //    case ValidationStatus.PasswordIsTooShort:
            //        await _userDialogs.AlertAsync(Resources["PasswordIsTooShort"], Resources["Oops"], Resources["Damn"]); break;
            //    case ValidationStatus.PasswordIsWeak:
            //        await _userDialogs.AlertAsync(Resources["PasswordIsWeak"], Resources["Oops"], Resources["Sure"]); break;
            //    case ValidationStatus.PasswordsAreNotEqual:
            //        await _userDialogs.AlertAsync(Resources["PasswordsAreNotEqual"], Resources["Oops"], Resources["Thanks"]); break;
            //    case ValidationStatus.Success:
            //        await _userDialogs.AlertAsync(Resources["RedirectingToSignIn"], Resources["Success"], Resources["Finally"]);
            //        ValidationSuccess();
            //        break;
            //    default:
            //        await _userDialogs.AlertAsync(Resources["Unknown"], Resources["Oops"], Resources["Damn"]); break;
            //}
            ValidationSuccess();
        }
        private async void ValidationSuccess()
        {
            _registrationService.Register(Name, Email, Password);
            NavigationParameters navParams = new NavigationParameters
            {
                { $"{nameof(Email)}", Email }
            };
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}", navParams);
        }


    }
}
