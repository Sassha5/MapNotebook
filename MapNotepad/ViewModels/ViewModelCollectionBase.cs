using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MapNotepad.Extensions;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class ViewModelCollectionBase : ViewModelBase
    {
        private readonly IPinsManagerService _pinsManagerService;

        private ObservableCollection<Pin> _PinCollection;
        public ObservableCollection<Pin> PinCollection
        {
            get => _PinCollection;
            set => SetProperty(ref _PinCollection, value);
        }

        public ViewModelCollectionBase(INavigationService navigationService,
                                       IPinsManagerService pinsManagerService)
                                       : base (navigationService)
        {
            _pinsManagerService = pinsManagerService;
        }

        protected void UpdateCollection()
        {
            var customPins = _pinsManagerService.GetUserPins(0);
            foreach (CustomPin pin in customPins)           //TODO make map work with custom pins
            {
                var newPin = new Pin() { Label = pin.Label, Position = new Position(pin.Latitude, pin.Longtitude) };
                PinCollection.Add(newPin);
            }
        }

        protected void AddPin(CustomPin pin)
        {
            _pinsManagerService.AddPin(pin);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UpdateCollection();
        }
    }
}
