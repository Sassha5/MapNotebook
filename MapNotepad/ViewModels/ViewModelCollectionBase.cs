using System.Collections.Generic;
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
            await UpdateCollectionAsync();
        }

        #endregion

        #region -- Commands --

        private ICommand _SearchBarTextChangedCommand;
        public ICommand SearchBarTextChangedCommand => _SearchBarTextChangedCommand ??= new Command(OnSearchBarTextChangedCommandAsync);

        #endregion

        #region -- Command Methods --

        private async void OnSearchBarTextChangedCommandAsync()
        {
            await UpdateCollectionAsync();
        }

        #endregion

        #region -- Protected Implementation --

        protected async Task UpdateCollectionAsync()
        {
            var pins = await GetPinsAsync();

            CustomPinCollection = new ObservableCollection<CustomPin>(pins);
        }

        protected async Task SavePinAsync(CustomPin pin)
        {
            await _pinsManagerService.SavePinAsync(pin);
            await UpdateCollectionAsync();
        }

        protected async Task DeletePinAsync(CustomPin pin)
        {
            await _pinsManagerService.DeletePinAsync(pin);
            await UpdateCollectionAsync();
        }

        protected virtual Task<IEnumerable<CustomPin>> GetPinsAsync()
        {
            return _pinsManagerService.GetCurrentUserPinsAsync(SearchBarText);
        }

        #endregion
    }
}
