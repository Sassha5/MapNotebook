using System;
using System.Windows.Input;
using MapNotepad.Extensions;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class AddPinPageViewModel : ViewModelCollectionBase
    {
        private CustomPin _customPin;

        #region Properties
        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }
        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }
        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }
        #endregion

        public AddPinPageViewModel(INavigationService navigationService,
                                IPinsManagerService pinsManagerService)
                                : base(navigationService, pinsManagerService)
        {
        }

        private ICommand _AddPinCommand;
        public ICommand AddPinCommand => _AddPinCommand ??= new Command(OnAddPinCommandAsync);

        private ICommand _MapClickedCommand;
        public ICommand MapClickedCommand => _MapClickedCommand ??= new Command<MapClickedEventArgs>(OnMapClickedCommand);

        private void OnMapClickedCommand(MapClickedEventArgs args)
        {
            UpdateCollection();
            Latitude = args.Point.Latitude;
            Longitude = args.Point.Longitude;
            _customPin = new CustomPin()
            {
                Label = string.Empty,
                Latitude = Latitude,
                Longitude = Longitude
            };
            IncomingPin = ModelsExtension.ToPin(_customPin);
        }

        private async void OnAddPinCommandAsync(object obj)
        {
            _customPin.Label = Label;
            _customPin.Description = Description;
            AddPin(_customPin);
            await NavigationService.GoBackAsync();
        }
    }
}
