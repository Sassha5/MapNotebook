using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Services.AuthorizationService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;

        public MainPageViewModel(INavigationService navigationService,
                                IAuthorizationService authorizationService,
                                IUserDialogs userDialogs)
                                : base(navigationService)
        {
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;
        }

        #region -- Commands --

        private ICommand _logoutCommand;
        public ICommand LogoutCommand => _logoutCommand ??= new Command(OnLogoutCommandAsync);

        private ICommand _settingsCommand;
        public ICommand SettingsCommand => _settingsCommand ??= new Command(OnSettingsCommandAsync);

        #endregion

        #region -- Command Methods --

        private async void OnSettingsCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(SettingsPage)}");
        }

        private async void OnLogoutCommandAsync()
        {
            bool result = await _userDialogs.ConfirmAsync("Are you sure?");
            if (result)
            {
                _authorizationService.Logout();
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
            }
        }

        #endregion
    }
}
