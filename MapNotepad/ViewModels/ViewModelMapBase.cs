using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        protected override void OnSearchCommand()
        {
            base.OnSearchCommand();
            UpdateMap();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            UpdateMap();
        }

        #endregion

        protected void UpdateMap()
        {
            PinCollection = IEnumerableExtension.ToObservableCollection(CustomPinCollection.Where(x => x.IsFavorite));
            MapStyle = _themeManagerService.GetCurrentMapStyle();
        }
    }
}
