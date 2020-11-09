using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Controls;
using MapNotepad.Models;
using MapNotepad.Services.NotificationService;
using MapNotepad.Services.PermissionService;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeService;
using Plugin.LocalNotifications;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class MapPageViewModel : ViewModelMapBase
    {
        private INotificationService _notificationService;

        public MapPageViewModel(INavigationService navigationService,
                               IPinsManagerService pinsManagerService,
                               IThemeService themeManagerService,
                               INotificationService notificationService,
                               IPermissionService permissionService)
                               : base(navigationService,
                                     pinsManagerService,
                                     themeManagerService,
                                     permissionService)
        {
            _notificationService = notificationService;
        }

        #region Properties

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set
            {
                SetProperty(ref _selectedPin, value);
                SetFrameProperties();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _reminder;
        public string Reminder
        {
            get => _reminder;
            set => SetProperty(ref _reminder, value);
        }

        #endregion

        private ICommand _addReminderCommand;
        public ICommand AddReminderCommand => _addReminderCommand ??= new Command(OnAddReminderCommandAsync);

        #region INavigatedAware override

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin pin))
            {
                SelectedPin = PinCollection.FirstOrDefault(x => x.Label == pin.Label);
                CameraPosition = new Position(pin.Latitude, pin.Longitude);
            }

            await _notificationService.SendPush("Hui", "v rotebal;");
        }

        #endregion

        private async void OnAddReminderCommandAsync()
        {
            if (_selectedPin != null)
            {
                await PopupNavigation.Instance.PushAsync(new AddReminderPopup());
                //var result = await UserDialogs.Instance.PromptAsync("Enter reminder:");
                //var customPin = CustomPinCollection.FirstOrDefault(x => x.Label == _selectedPin.Label);//TODO rework
                //customPin.Reminder = result.Text;
                //await SavePinAsync(customPin);
                //CrossLocalNotifications.Current.Show(customPin.Label, customPin.Reminder, 1, DateTime.Now.AddSeconds(5));
            }
        }

        private void SetFrameProperties()
        {
            var customPin = CustomPinCollection.FirstOrDefault(x => x.Label == _selectedPin.Label);
            if (customPin != null)
            {
                Description = customPin.Description;
                Reminder = customPin.Reminder;
            }
        }
    }
}
