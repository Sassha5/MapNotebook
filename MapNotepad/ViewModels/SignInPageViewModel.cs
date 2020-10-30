using System;
using System.Windows.Input;
using MapNotepad.Services.AuthorizationService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        private readonly IAuthorizationService _authorizationService;

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

        public SignInPageViewModel(INavigationService navigationService,
                               IAuthorizationService authorizationService)
                                  : base(navigationService)
        {
            _authorizationService = authorizationService;

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue($"{nameof(Email)}", out string email))
            {
                Email = email;
            }
        }

        private ICommand _SignUpCommand;
        public ICommand SignUpCommand => _SignUpCommand ??= new Command(OnSignUpCommandAsync);

        private ICommand _AuthorizeCommand;
        public ICommand AuthorizeCommand => _AuthorizeCommand ??= new Command(OnAuthorizeCommandAsync);

        private async void OnSignUpCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }

        private async void OnAuthorizeCommandAsync()
        {
            if (_authorizationService.TryAuthorize(Email, Password))
            {
                _authorizationService.Authorize(Email, Password);
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert(Resources["Oops"], Resources["WrongCredentials"], Resources["Ok"]);
                Password = string.Empty;
            }
        }
    }
}
