using System.Collections.ObjectModel;
using System.Windows.Input;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using Prism.Navigation;
using Xamarin.Forms;

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
        private ObservableCollection<CustomPin> _customPinCollection;
        public ObservableCollection<CustomPin> CustomPinCollection
        {
            get => _customPinCollection;
            set => SetProperty(ref _customPinCollection, value);
        }
        #endregion

        public ViewModelCollectionBase(INavigationService navigationService,
                                       IPinsManagerService pinsManagerService)
                                       : base(navigationService)
        {
            _pinsManagerService = pinsManagerService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UpdateCollection();
        }

        private ICommand _SearchCommand;
        public ICommand SearchCommand => _SearchCommand ??= new Command(OnSearchCommand);

        protected void OnSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchBarText))
            {
                CustomPinCollection = new ObservableCollection<CustomPin>(_pinsManagerService.GetCurrentUserPins(SearchBarText));
            }
            else
            {
                UpdateCollection();
            }
        }

        protected void UpdateCollection()
        {
            CustomPinCollection = new ObservableCollection<CustomPin>(_pinsManagerService.GetCurrentUserPins());
        }

        protected void SavePin(CustomPin pin)
        {
            _pinsManagerService.SavePin(pin);
        }

        protected void DeletePin(CustomPin pin)
        {
            _pinsManagerService.DeletePin(pin);
            UpdateCollection();
        }
    }
}
