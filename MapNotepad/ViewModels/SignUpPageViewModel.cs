using System.Windows.Input;
using MapNotepad.Services.RegistrationService;
using MapNotepad.Services.ValidationService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {

        private readonly IRegistrationService _registrationService;
        private readonly IValidationService _validationService;
        //private readonly IUserDialogs _userDialogs;

        public SignUpPageViewModel(INavigationService navigationService,
                                    IRegistrationService registrationService,
                                    IValidationService validationService)
                                    : base(navigationService)
        {
            //_userDialogs = userDialogs;
            _validationService = validationService;
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
            //switch (_validationService.ValidateNewUser(Email, Password, ConfirmPassword)) //TODO after localization
            //{
            //    case ValidationStatus.EmailIsTaken:
            //        await _userDialogs.AlertAsync(Resources["LoginIsTaken"], Resources["Oops"], Resources["Damn"]); break;
            //    case ValidationStatus.LoginIsTooShort:
            //        await _userDialogs.AlertAsync(Resources["LoginIsTooShort"], Resources["Oops"], Resources["Ok"]); break;
            //    case ValidationStatus.LoginStartsWithNumber:
            //        await _userDialogs.AlertAsync(Resources["LoginStartsWithNumber"], Resources["Oops"], Resources["Damn"]); break;
            //    case ValidationStatus.PasswordIsTooShort:
            //        await _userDialogs.AlertAsync(Resources["PasswordIsTooShort"], Resources["Oops"], Resources["Damn"]); break;
            //    case ValidationStatus.PasswordIsWeak:
            //        await _userDialogs.AlertAsync(Resources["PasswordIsWeak"], Resources["Oops"], Resources["Sure"]); break;
            //    case ValidationStatus.PasswordsAreNotEqual:
            //        await _userDialogs.AlertAsync(Resources["PasswordsAreNotEqual"], Resources["Oops"], Resources["Thanks"]); break;
            //    case ValidationStatus.Success:
            //        await _userDialogs.AlertAsync(Resources["RedirectingToSignIn"], Resources["Success"], Resources["Finally"]);
            //        RegistrationSuccess();
            //        break;
            //    default:
            //        await _userDialogs.AlertAsync(Resources["Unknown"], Resources["Oops"], Resources["Damn"]); break;
            //}

            RegistrationSuccess();
        }
        private async void RegistrationSuccess()
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
