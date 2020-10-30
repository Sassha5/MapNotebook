using System;
using System.Collections.Generic;
using MapNotepad.Services.SettingsManagerService;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly ISettingsManagerService _settingsManagerService;

        
        private bool _darkThemeIsChecked;
        public bool DarkThemeIsChecked
        {
            get { return _darkThemeIsChecked; }
            set
            {
                _darkThemeIsChecked = value;
                if (value) { _settingsManagerService.Theme = (int)OSAppTheme.Dark; }
                else { _settingsManagerService.Theme = (int)OSAppTheme.Light; }
                RaisePropertyChanged($"{nameof(DarkThemeIsChecked)}");
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (!string.IsNullOrEmpty(value)) { _selectedLanguage = value; }
                _settingsManagerService.Language = SelectedLanguage;
            }
        }
        public List<string> Languages { get; set; }

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
