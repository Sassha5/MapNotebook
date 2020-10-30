﻿using System;
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
    public class ViewModelCollectionBase : ViewModelBase
    {
        private readonly IPinsManagerService _pinsManagerService;

        #region Properties
        private string _searchBarText;
        public string SearchBarText
        {
            get => _searchBarText;
            set => SetProperty(ref _searchBarText, value);
        }
        private ObservableCollection<Pin> _pinCollection;
        public ObservableCollection<Pin> PinCollection
        {
            get => _pinCollection;
            set => SetProperty(ref _pinCollection, value);
        }
        #endregion
        public ViewModelCollectionBase(INavigationService navigationService,
                                       IPinsManagerService pinsManagerService)
                                       : base(navigationService)
        {
            _pinsManagerService = pinsManagerService;
        }

        protected void UpdateCollection()
        {
            //maybe overload with search parameter, also .Where(x => x.Favorite)
            PinCollection = new ObservableCollection<Pin>(IEnumerableExtension.ToObservableCollection(_pinsManagerService.GetCurrentUserPins()));
        }


        private ICommand _SearchCommand;
        public ICommand SearchCommand => _SearchCommand ??= new Command(OnSearchCommand);

        protected void OnSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchBarText))
            {
                PinCollection = new ObservableCollection<Pin>(IEnumerableExtension.ToObservableCollection(_pinsManagerService.GetCurrentUserPins(SearchBarText.ToLower())));
            }
            else
            {
                UpdateCollection();
            }
        }

        protected void AddPin(CustomPin pin)
        {
            _pinsManagerService.AddPin(pin);
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
