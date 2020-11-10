using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services.PermissionService;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeService;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class AddPinPageViewModel : ViewModelMapBase
    {
        private readonly IUserDialogs _userDialogs;

        public AddPinPageViewModel(INavigationService navigationService,
                                IPinsManagerService pinsManagerService,
                                IThemeService themeManagerService,
                                IPermissionService permissionService,
                                IUserDialogs userDialogs)
                                : base(navigationService,
                                      pinsManagerService,
                                      themeManagerService,
                                      permissionService)
        {
            _userDialogs = userDialogs;
        }

        #region Properties

        private CustomPin _customPin;
        public CustomPin CustomPin
        {
            get => _customPin;
            set => SetProperty(ref _customPin, value);
        }

        #endregion

        #region Commands

        private ICommand _savePinCommand;
        public ICommand SavePinCommand => _savePinCommand ??= new Command(OnSavePinCommandAsync);

        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand ??= new Command<Position>(OnMapClickedCommandAsync);

        #endregion

        #region INavigatedAware override

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            CustomPin = new CustomPin();
            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin customPin))
            {
                CustomPin = new CustomPin()
                {
                    Id = customPin.Id,
                    Label = customPin.Label,
                    Description = customPin.Description,
                    Latitude = customPin.Latitude,
                    Longitude = customPin.Longitude
                };
            }
        }

        #endregion

        #region Command execution methods

        private async void OnMapClickedCommandAsync(Position args)
        {
            await UpdateCollectionAsync();   //clear temporary pins

            CustomPin = new CustomPin()
            {
                Id = CustomPin.Id,
                Label = string.IsNullOrEmpty(CustomPin.Label) ? string.Empty : CustomPin.Label,
                Description = CustomPin.Description,
                IsFavorite = true,
                Latitude = args.Latitude,
                Longitude = args.Longitude
            };

            CustomPinCollection.Add(_customPin);                                 //add pin only in collection to show, not in repo
            CustomPinCollection = new ObservableCollection<CustomPin>(CustomPinCollection);//trigger property changed to show new pin
        }

        private async void OnSavePinCommandAsync()
        {
            if (!string.IsNullOrEmpty(_customPin.Label))
            {
                await SavePinAsync(_customPin);
                await NavigationService.GoBackAsync();
            }
            else
            {
                await _userDialogs.AlertAsync(Resources["InvalidLabel"]);
            }
        }

        #endregion
    }
}
