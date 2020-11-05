using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MapNotepad.Extensions;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeManagerService;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class ViewModelMapBase : ViewModelCollectionBase
    {
        private IThemeManagerService _themeManagerService;

        public ViewModelMapBase(INavigationService navigationService,
                                IPinsManagerService pinsManagerService,
                                IThemeManagerService themeManagerService)
                                : base(navigationService,
                                       pinsManagerService)
        {
            _themeManagerService = themeManagerService;
        }

        #region Properties

        private ObservableCollection<Pin> _pinCollection;
        public ObservableCollection<Pin> PinCollection
        {
            get => _pinCollection;
            set => SetProperty(ref _pinCollection, value);
        }

        private Position _cameraPosition;
        public Position CameraPosition
        {
            get => _cameraPosition;
            set => SetProperty(ref _cameraPosition, value);
        }

        private MapStyle _mapStyle;
        public MapStyle MapStyle
        {
            get => _mapStyle;
            set => SetProperty(ref _mapStyle, value);
        }

        #endregion

        #region Overrides

        protected override async Task SearchAsync()
        {
            await base.SearchAsync();

            await UpdateMap();
        }

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            await UpdateMap();
        }

        #endregion

        protected async Task UpdateMap()
        {
            PinCollection = _customPinCollection.Where(x => x.IsFavorite).ToObservableCollection();
            MapStyle = _themeManagerService.GetCurrentMapStyle();
        }
    }
}
