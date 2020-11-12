using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services.RegistrationService;
using MapNotepad.Validation;
using Prism.Common;
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

        #region-- Public Properties --

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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(User), out User user))
            {
                Name = user.Name;
                Email = user.Email;
            }
        }

        private ICommand _RegisterCommand;
        public ICommand RegisterCommand => _RegisterCommand ??= new Command(OnRegisterCommandAsync);

        private async void OnRegisterCommandAsync()
        {
            if (Validator.CheckEmail(Email))
            {
                if (Validator.CheckPassword(Password))
                {
                    if (Password.Equals(ConfirmPassword))
                    {
                        await _userDialogs.AlertAsync(Resources["RedirectingToSignIn"], Resources["Success"], Resources["Finally"]);
                        ValidationSuccess();
                    }
                    else
                    {
                        await _userDialogs.AlertAsync(Resources["PasswordsAreNotEqual"], Resources["Oops"], Resources["Damn"]);
                    }
                }
                else
                {
                    await _userDialogs.AlertAsync(Resources["BadPassword"], Resources["Oops"], Resources["Damn"]);
                }
            }
            else
            {
                await _userDialogs.AlertAsync(Resources["BadEmail"], Resources["Oops"], Resources["Damn"]);
            }
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
    }
}
