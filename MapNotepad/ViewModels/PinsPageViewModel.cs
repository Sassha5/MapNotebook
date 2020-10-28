using System;
using System.Windows.Input;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class PinsPageViewModel : ViewModelCollectionBase
    {

        public PinsPageViewModel(INavigationService navigationService,
                                 IPinsManagerService pinsManagerService)
                                 : base(navigationService,
                                        pinsManagerService)
        {
            //TODO onAppearing show pins
        }

        private ICommand _ToAddPinPageCommand;
        public ICommand ToAddPinPageCommand => _ToAddPinPageCommand ??= new Command(OnToAddPinPageCommandAsync);

        private async void OnToAddPinPageCommandAsync(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinPage)}");
        }
    }
}
