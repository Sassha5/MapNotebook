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

        private ObservableCollection<CustomPin> _pinCollection;
        public ObservableCollection<CustomPin> PinCollection
        {
            get => _pinCollection;
            set => SetProperty(ref _pinCollection, value);
        }

        private Pin _incomingPin;
        public Pin IncomingPin
        {
            get => _incomingPin;
            set => SetProperty(ref _incomingPin, value);
        }

        public ViewModelCollectionBase(INavigationService navigationService,
                                       IPinsManagerService pinsManagerService)
                                       : base (navigationService)
        {
            _pinsManagerService = pinsManagerService;
        }

        protected void UpdateCollection()
        {
            var customPins = _pinsManagerService.GetCurrentUserPins(); //maybe overload with search parameter, also .Where(x => x.Favorite)
            PinCollection = new ObservableCollection<CustomPin>(customPins);
            foreach (CustomPin pin in customPins)
            {
                IncomingPin = ModelsExtension.ToPin(pin);
            }
        }

        protected void AddPin(CustomPin pin)
        {
            _pinsManagerService.AddPin(pin);
            UpdateCollection();
        }

        protected void DeletePin(CustomPin pin)
        {
            _pinsManagerService.DeletePin(pin);
            UpdateCollection();
        }
            
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UpdateCollection();
        }
    }
}
