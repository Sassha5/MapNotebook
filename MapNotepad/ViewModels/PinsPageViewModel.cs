using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Views;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class PinsPageViewModel : ViewModelBase
    {
        private readonly IPinsManagerService _pinsManagerService;


        private ObservableCollection<CustomPin> _CustomPinCollection = new ObservableCollection<CustomPin>();
        public ObservableCollection<CustomPin> CustomPinCollection
        {
            get => _CustomPinCollection;
            set => SetProperty(ref _CustomPinCollection, value);
        }

        public PinsPageViewModel(INavigationService navigationService,
                                 IPinsManagerService pinsManagerService)
                                 : base(navigationService)
        {
            _pinsManagerService = pinsManagerService;
        }

        private ICommand _ToAddPinPageCommand;
        public ICommand ToAddPinPageCommand => _ToAddPinPageCommand ??= new Command(OnToAddPinPageCommandAsync);

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand => _DeleteCommand ??= new Command<CustomPin>(OnDeleteCommandAsync);

        private ICommand _SearchCommand;
        public ICommand SearchCommand => _SearchCommand ??= new Command<string>(OnSearchCommandAsync);

        private ICommand _PinTappedCommand;
        public ICommand PinTappedCommand => _PinTappedCommand ??= new Command<CustomPin>(OnPinTappedCommandAsync);

        private async void OnPinTappedCommandAsync(CustomPin pin)
        {
            NavigationParameters navParams = new NavigationParameters
            {
                { nameof(CustomPin), pin }
            };
            await NavigationService.NavigateAsync($"{nameof(MainPage)}?selectedTab={nameof(MapPage)}", navParams);
        }

        private void OnSearchCommandAsync(string searchValue)
        {
            CustomPinCollection.Clear(); //TODO fix this shit
            if (searchValue != string.Empty)
            {
                CustomPinCollection = new ObservableCollection<CustomPin>(_pinsManagerService.GetCurrentUserPins(searchValue.ToLower()));
            }
            else
            {
                UpdateCollection();
            }
        }

        private void UpdateCollection()
        {
            var customPins = _pinsManagerService.GetCurrentUserPins(); //maybe overload with search parameter
            CustomPinCollection.Clear();
            foreach (CustomPin pin in customPins)
            {
                CustomPinCollection.Add(pin);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UpdateCollection();
        }

        private async void OnToAddPinPageCommandAsync(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinPage)}");
        }

        private async void OnDeleteCommandAsync(CustomPin pin)
        {
            //bool result = await Confirm();
            //if (result)
            _pinsManagerService.DeletePin(pin);
            UpdateCollection();
            //}
        }
    }
}
