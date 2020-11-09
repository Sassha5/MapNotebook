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

        private CustomPin _selectedCustomPin;
        public CustomPin SelectedCustomPin
        {
            get => _selectedCustomPin;
            set => SetProperty(ref _selectedCustomPin, value);
        }

        #endregion

        private ICommand _addReminderCommand;
        public ICommand AddReminderCommand => _addReminderCommand ??= new Command(OnAddReminderCommandAsync);

        private ICommand _selectedPinChangedCommand;
        public ICommand SelectedPinChangedCommand => _selectedPinChangedCommand ??= new Command<object>(OnSelectedPinChangedCommand);

        #region INavigatedAware override

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin pin))
            {
                SelectedCustomPin = pin;
                CameraPosition = new Position(pin.Latitude, pin.Longitude);
            }

            //await _notificationService.SendPush("Hui", "v rotebal;");
        }

        #endregion

        private async void OnAddReminderCommandAsync()
        {
            if (_selectedCustomPin != null)
            {
                await PopupNavigation.Instance.PushAsync(new AddReminderPopup());
                //var result = await UserDialogs.Instance.PromptAsync("Enter reminder:");
                //var customPin = CustomPinCollection.FirstOrDefault(x => x.Label == _selectedPin.Label);//TODO rework
                //customPin.Reminder = result.Text;
                //await SavePinAsync(customPin);
                //CrossLocalNotifications.Current.Show(customPin.Label, customPin.Reminder, 1, DateTime.Now.AddSeconds(5));
            }
        }


        private void OnSelectedPinChangedCommand(object pinObj)
        {
            if (pinObj is Pin pin)
            {
                SelectedCustomPin = CustomPinCollection.FirstOrDefault(x => x.Label == pin.Label);
            }
        }
    }
}
