using System;
using System.Collections.ObjectModel;
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

        private ICommand _SavePinCommand;
        public ICommand SavePinCommand => _SavePinCommand ??= new Command(OnSavePinCommandAsync);

        private ICommand _MapClickedCommand;
        public ICommand MapClickedCommand => _MapClickedCommand ??= new Command<MapClickedEventArgs>(OnMapClickedCommand);

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _customPin = new CustomPin()
            {
                Label = string.Empty,
                IsFavorite = true
            };
            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin customPin))
            {
                _customPin.Id = customPin.Id;
                Label = customPin.Label;
                Description = customPin.Description;
                Latitude = customPin.Latitude;
                Longitude = customPin.Longitude;
            }
        }

        private void OnMapClickedCommand(MapClickedEventArgs args)//wtf it works
        {
            UpdateCollection();   //clear temporary pins
            Latitude = args.Point.Latitude;
            Longitude = args.Point.Longitude;

            _customPin.Latitude = Latitude;
            _customPin.Longitude = Longitude;
                
            PinCollection.Add(ModelsExtension.ToPin(_customPin)); //add pin only in collection to show, not in repo
            PinCollection = new ObservableCollection<Pin>(PinCollection); //trigger property changed to show new pin
        }

        private async void OnSavePinCommandAsync(object obj)
        {
            _customPin.Label = Label;
            _customPin.Description = Description;
            _customPin.Latitude = Latitude;
            _customPin.Longitude = Longitude;

            SavePin(_customPin);
            await NavigationService.GoBackAsync();
        }
    }
}
