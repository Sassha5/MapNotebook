using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MapNotepad.Extensions;
using MapNotepad.Services.PermissionService;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeService;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class ViewModelMapBase : ViewModelCollectionBase
    {
        private IThemeService _themeManagerService;
        protected IPermissionService _permissionService;

        public ViewModelMapBase(INavigationService navigationService,
                                IPinsManagerService pinsManagerService,
                                IThemeService themeManagerService,
                                IPermissionService permissionService)
                                : base(navigationService,
                                       pinsManagerService)
        {
            _themeManagerService = themeManagerService;
            _permissionService = permissionService;
        }

        #region Properties

        //private ObservableCollection<Pin> _pinCollection;
        //public ObservableCollection<Pin> PinCollection
        //{
        //    get => _pinCollection;
        //    set => SetProperty(ref _pinCollection, value);
        //}

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

        private bool _locationGranted;
        public bool LocationGranted
        {
            get => _locationGranted;
            set => SetProperty(ref _locationGranted, value);
        }

        #endregion

        #region Overrides

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            if (!LocationGranted)
            {
                LocationGranted = await _permissionService.RequestPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Granted;
            }

            await base.OnNavigatedToAsync(parameters);

            MapStyle = _themeManagerService.GetCurrentMapStyle();
        }

        #endregion
    }
}
