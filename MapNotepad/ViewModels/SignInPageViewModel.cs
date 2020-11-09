using System;
using System.Windows.Input;
using MapNotepad.Services.AuthorizationService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Auth;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        private readonly IAuthorizationService _authorizationService;
        //private readonly ITwitterAuthorizationService _twitterAuthorizationService;

        public SignInPageViewModel(INavigationService navigationService,
                                   IAuthorizationService authorizationService)
                                   : base(navigationService)
        {
            _authorizationService = authorizationService;
            //_twitterAuthorizationService = twitterAuthorizationService;
        }

        #region Properties

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

        #region INavigatedAware override

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue($"{nameof(Email)}", out string email))
            {
                Email = email;
            }
        }

        #endregion

        #region Commands

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new Command(OnSignUpCommandAsync);

        private ICommand _authorizeCommand;
        public ICommand AuthorizeCommand => _authorizeCommand ??= new Command(OnAuthorizeCommandAsync);

        private ICommand _twitterSignInCommand;
        public ICommand TwitterSignInCommand => _twitterSignInCommand ??= new Command(OnTwitterSignInCommandAsync);

        #endregion

        #region Command execution methods

        private async void OnSignUpCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }

        private async void OnAuthorizeCommandAsync()
        {
            var canAuthorize = await _authorizationService.CanAuthorizeAsync(Email, Password);
            if (canAuthorize)
            {
                _authorizationService.AuthorizeAsync(Email, Password);
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(Resources["Oops"], Resources["WrongCredentials"], Resources["Ok"]);
                Password = string.Empty;
            }
        }

        private void OnTwitterSignInCommandAsync()
        {
            //_twitterAuthorizationService.Login();
        }

        #endregion
    }
}
