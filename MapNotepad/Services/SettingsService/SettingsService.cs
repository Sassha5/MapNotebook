using MapNotepad.Localization;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

namespace MapNotepad.Services.SettingsService
{
    class SettingsService : ISettingsService
    {
        private readonly ISettings _appSettings;

        public SettingsService(ISettings appSettings)
        {
            _appSettings = appSettings;
        }

        #region -- ISettingsService Implementation --

        public string AuthorizedUserID
        {
            get => _appSettings.GetValueOrDefault(nameof(AuthorizedUserID), Constants.NoAuthorizedUser);
            set => _appSettings.AddOrUpdateValue(nameof(AuthorizedUserID), value);
        }

        public int Theme
        {
            get => _appSettings.GetValueOrDefault(nameof(Theme), (int)OSAppTheme.Dark);
            set => _appSettings.AddOrUpdateValue(nameof(Theme), value);
        }

        public string Language
        {
            get => _appSettings.GetValueOrDefault(nameof(Language), Constants.DefaultLanguage);
            set
            {
                _appSettings.AddOrUpdateValue(nameof(Language), value);
                MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(value));
            }
        }

        public void ClearData()
        {
            AuthorizedUserID = Constants.NoAuthorizedUser;
            Theme = (int)OSAppTheme.Dark;
            Language = Constants.DefaultLanguage;
        }

        #endregion
    }
}
