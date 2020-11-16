using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services.AuthorizationService;
using MapNotepad.Services.AuthorizationService.Twitter;
using MapNotepad.Services.RegistrationService.Facebook;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class SignInPageViewModel : ViewModelBase, IAuthDelegate
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ITwitterAuthorizationService _twitterAuthorizationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IFacebookService _facebookService;

        public SignInPageViewModel(INavigationService navigationService,
                                   ITwitterAuthorizationService twitterAuthorizationService,
                                   IAuthorizationService authorizationService,
                                   IUserDialogs userDialogs,
                                   IFacebookService facebookService)
                                   : base(navigationService)
        {
            _authorizationService = authorizationService;
            _twitterAuthorizationService = twitterAuthorizationService;
            _twitterAuthorizationService.RegisterAuthDelegate(this);
            _userDialogs = userDialogs;
            _facebookService = facebookService;
            _facebookService.RegisterAuthDelegate(this);
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

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue($"{nameof(Email)}", out string email))
            {
                Email = email;
            }
        }

        #endregion

        #region -- Commands --

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new Command(OnSignUpCommandAsync);

        private ICommand _authorizeCommand;
        public ICommand AuthorizeCommand => _authorizeCommand ??= new Command(OnAuthorizeCommandAsync);

        private ICommand _facebookSignUpCommand;
        public ICommand FacebookSignUpCommand => _facebookSignUpCommand ??= new Command(OnFacebookSignUpCommand);

        private ICommand _twitterSignInCommand;
        public ICommand TwitterSignInCommand => _twitterSignInCommand ??= new Command(OnTwitterSignInCommand);

        #endregion

        #region -- Command Methods --

        private async void OnSignUpCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }

        private async void OnAuthorizeCommandAsync()
        {
            bool canAuthorize = await _authorizationService.CanAuthorizeAsync(Email, Password);
            if (canAuthorize)
            {
                await _authorizationService.AuthorizeAsync(Email, Password);
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
            }
            else
            {
                await _userDialogs.AlertAsync(Resources["WrongCredentials"], Resources["Oops"], Resources["Ok"]);
                Password = string.Empty;
            }
        }

        private void OnTwitterSignInCommand()
        {
            _twitterAuthorizationService.Login();
        }

        private void OnFacebookSignUpCommand()
        {
            _facebookService.TryGetUserInfoAsync();
        }

        #endregion

        #region -- Private Helpers --

        public async void AuthSuccess(AuthResult result)
        {
            if (string.IsNullOrEmpty(result.Email)) //true if called from twitter
            {
                _authorizationService.AuthorizeAsync(result.UserID);
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
            }
            else
            {
                User user = new User()
                {
                    Email = result.Email,
                    Name = result.Username
                };

                NavigationParameters navParams = new NavigationParameters()
                {
                    { nameof(User), user }
                };

                await NavigationService.NavigateAsync($"{nameof(SignUpPage)}", navParams);
            }
        }

        public void AuthFailure()
        {
            _userDialogs.AlertAsync(Resources["AuthorizationUnsuccessful"], Resources["Nope"], Resources["Ok"]);
        }

        #endregion
    }
}
