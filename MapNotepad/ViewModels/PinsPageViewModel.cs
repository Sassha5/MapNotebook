using System;
using System.Windows.Input;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class PinsPageViewModel : ViewModelCollectionBase
    {
        public PinsPageViewModel(INavigationService navigationService,
                                 IPinsManagerService pinsManagerService)
                                 : base(navigationService,
                                        pinsManagerService)
        {
        }

        private ICommand _ToAddPinPageCommand;
        public ICommand ToAddPinPageCommand => _ToAddPinPageCommand ??= new Command(OnToAddPinPageCommandAsync);

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand => _DeleteCommand ??= new Command<Pin>(OnDeleteCommandAsync);

        private ICommand _EditCommand;
        public ICommand EditCommand => _EditCommand ??= new Command<Pin>(OnEditCommandAsync);

        private ICommand _PinTappedCommand;
        public ICommand PinTappedCommand => _PinTappedCommand ??= new Command<Pin>(OnPinTappedCommandAsync);

        private async void OnPinTappedCommandAsync(Pin pin)
        {
            NavigationParameters navParams = new NavigationParameters
            {
                { nameof(Pin), pin }
            };
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}?selectedTab={nameof(MapPage)}", navParams);
        }

        private async void OnToAddPinPageCommandAsync(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinPage)}");
        }

        private async void OnDeleteCommandAsync(Pin pin)
        {
            //bool result = await Confirm();
            //if (result)
            DeletePin(pin);
            //}
        }

        private async void OnEditCommandAsync(Pin pin)
        {
            NavigationParameters navParams = new NavigationParameters
            {
                { nameof(CustomPin), GetCustomPin(pin) }
            };                                              //TODO fix map to not show old pin location
            await NavigationService.NavigateAsync(nameof(AddPinPage), navParams);
        }
    }
}
