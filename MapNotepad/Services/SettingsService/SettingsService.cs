using MapNotepad.Localization;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

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

        public int AuthorizedUserID
        {
            get => _appSettings.GetValueOrDefault(nameof(AuthorizedUserID), Constants.NoAuthorizedUser);
            set => _appSettings.AddOrUpdateValue(nameof(AuthorizedUserID), value);
        }

        public int Theme
        {
            get => _appSettings.GetValueOrDefault(nameof(Theme), (int)OSAppTheme.Light);
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

        public Position LastMapPosition
        {
            get => GetPosition();
            set => SavePosition(value);
        }

        public void ClearData()
        {
            AuthorizedUserID = Constants.NoAuthorizedUser;
            Theme = (int)OSAppTheme.Light;
            Language = Constants.DefaultLanguage;
        }

        #endregion

        #region -- Private Helpers --

        private double positionLatitude;
        private double positionLongitude;

        private void SavePosition(Position position)
        {
            _appSettings.AddOrUpdateValue(nameof(positionLatitude), position.Latitude);
            _appSettings.AddOrUpdateValue(nameof(positionLongitude), position.Longitude);
        }

        private Position GetPosition()
        {
            positionLatitude = _appSettings.GetValueOrDefault(nameof(positionLatitude), 0);
            positionLongitude = _appSettings.GetValueOrDefault(nameof(positionLatitude), 0);

            return new Position(positionLatitude, positionLatitude);
        }

        #endregion
    }
}
