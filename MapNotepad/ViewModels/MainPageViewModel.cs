using System.ComponentModel;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Services.AuthorizationService;
using MapNotepad.Services.ThemeService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IThemeService _themeService;
        private readonly IUserDialogs _userDialogs;

        public MainPageViewModel(INavigationService navigationService,
                                IAuthorizationService authorizationService,
                                IThemeService themeService,
                                IUserDialogs userDialogs)
                                : base(navigationService)
        {
            _authorizationService = authorizationService;
            _themeService = themeService;
            _userDialogs = userDialogs;

            DarkThemeIsEnabled = _themeService.GetCurrentTheme() == OSAppTheme.Dark;
        }

        #region -- Public Properties --

        public string ThemeIconPath => DarkThemeIsEnabled ? Resources["DarkThemeIconPath"]
                                                          : Resources["LightThemeIconPath"];

        private bool _darkThemeIsEnabled;
        public bool DarkThemeIsEnabled
        {
            get => _darkThemeIsEnabled;
            set => SetProperty(ref _darkThemeIsEnabled, value);
        }

        #endregion

        #region -- Commands --

        private ICommand _logoutCommand;
        public ICommand LogoutCommand => _logoutCommand ??= new Command(OnLogoutCommandAsync);

        private ICommand _changeThemeCommand;
        public ICommand ChangeThemeCommand => _changeThemeCommand ??= new Command(OnChangeThemeCommand);

        #endregion

        #region -- Command Methods --

        private void OnChangeThemeCommand()
        {
            DarkThemeIsEnabled = !DarkThemeIsEnabled;
            MessagingCenter.Send<object>(this, Constants.ThemeChangedMessage);
        }

        private async void OnLogoutCommandAsync()
        {
            bool result = await _userDialogs.ConfirmAsync(Resources["SureQuestion"]);
            if (result)
            {
                _authorizationService.Logout();
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
            }
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(DarkThemeIsEnabled))
            {
                if (_darkThemeIsEnabled)
                {
                    _themeService.SetApplicationTheme(OSAppTheme.Dark);
                }
                else
                {
                    _themeService.SetApplicationTheme(OSAppTheme.Light);
                }

                RaisePropertyChanged($"{nameof(ThemeIconPath)}");
            }
        }

        #endregion
    }
}
