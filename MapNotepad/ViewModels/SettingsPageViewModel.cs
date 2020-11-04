using MapNotepad.Services.ThemeManagerService;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly IThemeManagerService _themeManagerService;

        public SettingsPageViewModel(INavigationService navigationService,
                                     IThemeManagerService themeManagerService)
                                     : base(navigationService)
        {
            _themeManagerService = themeManagerService;
        }

        #region Properties

        private bool _darkThemeIsChecked;
        public bool DarkThemeIsChecked
        {
            get => _darkThemeIsChecked; 
            set
            {
                SetProperty(ref _darkThemeIsChecked, value);
                if (value) { _themeManagerService.SetApplicationTheme(OSAppTheme.Dark); }
                else { _themeManagerService.SetApplicationTheme(OSAppTheme.Light); }
            }
        }

        #endregion

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                DarkThemeIsChecked = true;
            }
        }
    }
}
