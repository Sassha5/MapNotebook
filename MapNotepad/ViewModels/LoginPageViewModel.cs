using System;
using System.Windows.Input;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService):base(navigationService)
        {

        }

        private ICommand _AuthorizeCommand;
        public ICommand AuthorizeCommand => new Command(OnAuthorizeCommandAsync);

        private async void OnAuthorizeCommandAsync(object obj)
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
        }
    }
}
