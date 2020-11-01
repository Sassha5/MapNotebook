using System.Collections.Generic;
using MapNotepad.Services.SettingsManagerService;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly ISettingsManagerService _settingsManagerService;

        #region Properties
        private bool _darkThemeIsChecked;
        public bool DarkThemeIsChecked
        {
            get => _darkThemeIsChecked; 
            set
            {
                SetProperty(ref _darkThemeIsChecked, value);
                if (value) { _settingsManagerService.Theme = (int)OSAppTheme.Dark; }
                else { _settingsManagerService.Theme = (int)OSAppTheme.Light; }
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (!string.IsNullOrEmpty(value)) { _selectedLanguage = value; }
                _settingsManagerService.Language = SelectedLanguage;
            }
        }
        public List<string> Languages { get; set; }
        #endregion

        public SettingsPageViewModel(INavigationService navigationService,
                                     ISettingsManagerService settingsManagerService)
                                     : base(navigationService)
        {
            _settingsManagerService = settingsManagerService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                DarkThemeIsChecked = true;
            }
        }
    }
}
