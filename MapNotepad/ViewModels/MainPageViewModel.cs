using System;
using System.Windows.Input;
using MapNotepad.Services.AuthorizationService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAuthorizationService _authorizationService;

        public MainPageViewModel(INavigationService navigationService,
                                IAuthorizationService authorizationService)
                                : base(navigationService)
        {
            _authorizationService = authorizationService;
        }

        private ICommand _LogoutCommand;
        public ICommand LogoutCommand => _LogoutCommand ??= new Command(OnLogoutCommandAsync);

        private ICommand _SettingsCommand;
        public ICommand SettingsCommand => _SettingsCommand ??= new Command(OnSettingsCommandAsync);

        private async void OnSettingsCommandAsync(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(SettingsPage)}");
        }

        private async void OnLogoutCommandAsync(object obj)
        {
            _authorizationService.Logout();
            await NavigationService.NavigateAsync($"/{nameof(SignInPage)}");
        }
    }
}
