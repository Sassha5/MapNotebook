namespace MapNotepad.Services.SettingsService
{
    public interface ISettingsService
    {
        string AuthorizedUserID { get; set; }
        int Theme { get; set; }
        string Language { get; set; }
        void ClearData();
    }
}
