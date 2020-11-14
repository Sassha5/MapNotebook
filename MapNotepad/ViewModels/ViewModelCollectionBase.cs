using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class ViewModelCollectionBase : ViewModelBase
    {
        protected readonly IPinsManagerService _pinsManagerService;

        public ViewModelCollectionBase(INavigationService navigationService,
                                       IPinsManagerService pinsManagerService)
                                       : base(navigationService)
        {
            _pinsManagerService = pinsManagerService;
        }


        #region -- Public Properties --

        private string _searchBarText;
        public string SearchBarText
        {
            get => _searchBarText;
            set => SetProperty(ref _searchBarText, value);
        }

        protected ObservableCollection<CustomPin> _customPinCollection;
        public ObservableCollection<CustomPin> CustomPinCollection
        {
            get => _customPinCollection;
            set => SetProperty(ref _customPinCollection, value);
        }

        #endregion

        #region -- Overrides --

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            SearchBarText = string.Empty;
            await UpdateCollectionAsync();
        }

        #endregion

        #region -- Commands --

        private ICommand _SearchBarTextChangedCommand;
        public ICommand SearchCommand => _SearchBarTextChangedCommand ??= new Command(OnSearchCommandAsync);

        #endregion

        #region -- Command Methods --

        private async void OnSearchCommandAsync()
        {
            await UpdateCollectionAsync();
        }

        #endregion

        #region -- Protected Implementation --

        protected async Task UpdateCollectionAsync()
        {
            var pins = await _pinsManagerService.GetCurrentUserPinsAsync(SearchBarText);
            
            CustomPinCollection = new ObservableCollection<CustomPin>(pins);
        }

        protected async Task SavePinAsync(CustomPin pin)
        {
            await _pinsManagerService.SavePinAsync(pin);
            await UpdateCollectionAsync(); //only to change IsFavorite image
        }

        protected async Task DeletePinAsync(CustomPin pin)
        {
            await _pinsManagerService.DeletePinAsync(pin);
            await UpdateCollectionAsync();
        }

        #endregion
    }
}
