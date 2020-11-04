using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services.ThemeManagerService
{
    public interface IThemeManagerService
    {
        void SetApplicationTheme(OSAppTheme theme);
        void SetPreviousTheme();
        OSAppTheme GetCurrentTheme();
        MapStyle GetCurrentMapStyle();
    }
}
