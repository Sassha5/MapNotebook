using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MapNotepad.Extensions;
using MapNotepad.Services.PermissionService;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeManagerService;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class ViewModelMapBase : ViewModelCollectionBase
    {
        private IThemeManagerService _themeManagerService;
        private IPermissionService _permissionService;

        public ViewModelMapBase(INavigationService navigationService,
                                IPinsManagerService pinsManagerService,
                                IThemeManagerService themeManagerService,
                                IPermissionService permissionService)
                                : base(navigationService,
                                       pinsManagerService)
        {
            _themeManagerService = themeManagerService;
            _permissionService = permissionService;
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

        private bool _LocationGranted;
        public bool LocationGranted
        {
            get => _LocationGranted;
            set => SetProperty(ref _LocationGranted, value);
        }

        #endregion

        #region Overrides

        protected override async Task SearchAsync()
        {
            await base.SearchAsync();

            UpdateMap();
        }

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            if (!LocationGranted)
            {
                LocationGranted = await _permissionService.RequestPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Granted;
            }

            await base.OnNavigatedToAsync(parameters);

            UpdateMap();
        }

        #endregion

        protected void UpdateMap()
        {
            PinCollection = _customPinCollection.Where(x => x.IsFavorite).ToObservableCollection();
            MapStyle = _themeManagerService.GetCurrentMapStyle();
        }
    }
}
