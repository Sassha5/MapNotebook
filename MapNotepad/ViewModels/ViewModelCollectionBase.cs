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


        #region Properties

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


        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await UpdateCollectionAsync();
        }

        private ICommand _SearchCommand;
        public ICommand SearchCommand => _SearchCommand ??= new Command(OnSearchCommandAsync);

        #region Protected implementation

        protected async Task UpdateCollectionAsync()
        {
            var pins = await _pinsManagerService.GetCurrentUserPinsAsync();
            
            CustomPinCollection = new ObservableCollection<CustomPin>(pins);
        }

        protected async Task<int> SavePinAsync(CustomPin pin)
        {
            int id = await _pinsManagerService.SavePinAsync(pin);
            await UpdateCollectionAsync();
            return id;
        }

        protected async Task<int> DeletePinAsync(CustomPin pin)
        {
            int id = await _pinsManagerService.DeletePinAsync(pin);
            await UpdateCollectionAsync();
            return id;
        }

        protected async virtual Task SearchAsync()
        {
            if (!string.IsNullOrEmpty(SearchBarText))
            {
                var pins = await _pinsManagerService.GetCurrentUserPinsAsync(SearchBarText);
                CustomPinCollection = new ObservableCollection<CustomPin>(pins);
            }
            else
            {
                await UpdateCollectionAsync();
            }
        }

        #endregion

        private async void OnSearchCommandAsync()
        {
            await SearchAsync();
        }
    }
}
