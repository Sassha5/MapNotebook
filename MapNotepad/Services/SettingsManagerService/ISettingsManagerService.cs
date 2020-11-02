using System;
namespace MapNotepad.Services.SettingsManagerService
{
    public interface ISettingsManagerService
    {
        int AuthorizedUserID { get; set; }
        int Theme { get; set; }
        string Language { get; set; }
        void ClearData();
    }
}
