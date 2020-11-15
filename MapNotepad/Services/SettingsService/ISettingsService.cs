namespace MapNotepad.Services.SettingsService
{
    public interface ISettingsService
    {
        int AuthorizedUserID { get; set; }
        int Theme { get; set; }
        string Language { get; set; }
        void ClearData();
    }
}
