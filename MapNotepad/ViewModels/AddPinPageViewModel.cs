using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Extensions;
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

        //private string _label;
        //public string Label
        //{
        //    get => _label;
        //    set => SetProperty(ref _label, value);
        //}

        //private string _description;
        //public string Description
        //{
        //    get => _description;
        //    set => SetProperty(ref _description, value);
        //}

        //private double _latitude;
        //public double Latitude
        //{
        //    get => _latitude;
        //    set => SetProperty(ref _latitude, value);
        //}

        //private double _longitude;
        //public double Longitude
        //{
        //    get => _longitude;
        //    set => SetProperty(ref _longitude, value);
        //}

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

        private async void OnMapClickedCommandAsync(Position args)//wtf it works
        {
            await UpdateCollectionAsync();   //clear temporary pins

            CustomPin = new CustomPin()
            {
                Label = string.IsNullOrEmpty(CustomPin.Label) ? "New Pin" : CustomPin.Label,
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
                //_customPin.Label = Label;
                //_customPin.Description = Description;
                //_customPin.Latitude = Latitude;
                //_customPin.Longitude = Longitude;//how to check is valid?

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
