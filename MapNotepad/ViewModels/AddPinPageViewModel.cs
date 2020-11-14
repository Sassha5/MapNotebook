using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services.PermissionService;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeService;
using MapNotepad.Validation;
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

        #region -- Public Properties --

        private CustomPin _customPin;
        public CustomPin CustomPin
        {
            get => _customPin;
            set => SetProperty(ref _customPin, value);
        }

        #endregion

        #region -- Commands --

        private ICommand _savePinCommand;
        public ICommand SavePinCommand => _savePinCommand ??= new Command(OnSavePinCommandAsync);

        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand ??= new Command<Position>(OnMapClickedCommandAsync);

        #endregion

        #region -- Overrides --

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

        #region -- Command Methods --

        private async void OnMapClickedCommandAsync(Position args)
        {
            await UpdateCollectionAsync();   //clear temporary pins

            CustomPin = new CustomPin()
            {
                Id = CustomPin.Id,
                Label = CustomPin.Label == null ? string.Empty : CustomPin.Label,
                Description = CustomPin.Description == null ? string.Empty : CustomPin.Description,
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
                if (Validator.CheckLatitude(_customPin.Latitude) && Validator.CheckLongitude(_customPin.Longitude))
                {
                    await SavePinAsync(_customPin);
                    await NavigationService.GoBackAsync();
                }
                else
                {
                    await _userDialogs.AlertAsync(Resources["InvalidPosition"]);
                }
            }
            else
            {
                await _userDialogs.AlertAsync(Resources["InvalidLabel"]);
            }
        }

        #endregion
    }
}
