using System.Reflection;
using MapNotepad.Services.SettingsService;
using MapNotepad.Views;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services.ThemeService
{
    public class ThemeService : IThemeService
    {
        private ISettingsService _settingsManagerService;
        private string _darkMapStyleFile;

        public ThemeService(ISettingsService settingsManagerService)
        {
            _settingsManagerService = settingsManagerService;

            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"MapNotepad.Resources.DarkMapStyle.json");
            using var reader = new System.IO.StreamReader(stream);
            _darkMapStyleFile = reader.ReadToEnd();
        }

        public void SetPreviousTheme()
        {
            Application.Current.UserAppTheme = (OSAppTheme)_settingsManagerService.Theme;
        }

        public void SetApplicationTheme(OSAppTheme theme)
        {
            Application.Current.UserAppTheme = theme;
            _settingsManagerService.Theme = (int)theme;
        }

        public OSAppTheme GetCurrentTheme()
        {
            return (OSAppTheme)_settingsManagerService.Theme;
        }

        public MapStyle GetCurrentMapStyle()
        {
            if (GetCurrentTheme() == OSAppTheme.Dark)
            {
                return MapStyle.FromJson(_darkMapStyleFile);
            }
            else
            {
                return null;
            }
        }
    }
}
