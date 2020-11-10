using System;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services.SettingsService
{
    public interface ISettingsService
    {
        int AuthorizedUserID { get; set; }
        int Theme { get; set; }
        string Language { get; set; }
        Position LastMapPosition { get; set; }
        void ClearData();
    }
}
