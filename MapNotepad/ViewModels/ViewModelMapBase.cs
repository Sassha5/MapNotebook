using System;
using System.Collections.ObjectModel;
using System.Linq;
using MapNotepad.Extensions;
using MapNotepad.Services.PinsManagerService;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class ViewModelMapBase : ViewModelCollectionBase
    {
        public ViewModelMapBase(INavigationService navigationService,
                                IPinsManagerService pinsManagerService)
                                : base(navigationService,
                                       pinsManagerService)
        {
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

        #endregion

        #region Overrides

        protected override void OnSearchCommand()
        {
            base.OnSearchCommand();
            UpdateMap();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters); //TODO favorite not working because checkbox changes locally and sets back after updating collection from repo
            UpdateMap();
        }

        #endregion

        protected void UpdateMap()
        {
            PinCollection = IEnumerableExtension.ToObservableCollection(CustomPinCollection.Where(x => x.IsFavorite));
        }
    }
}
