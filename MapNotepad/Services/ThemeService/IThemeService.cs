using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services.ThemeService
{
    public interface IThemeService
    {
        void SetApplicationTheme(OSAppTheme theme);
        void SetPreviousTheme();
        OSAppTheme GetCurrentTheme();
        MapStyle GetCurrentMapStyle();
    }
}
