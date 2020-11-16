using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Views;
using Prism.Common;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class PinsPageViewModel : ViewModelCollectionBase
    {
        private IUserDialogs _userDialogs;

        public PinsPageViewModel(INavigationService navigationService,
                                 IPinsManagerService pinsManagerService,
                                 IUserDialogs userDialogs)
                                 : base(navigationService,
                                        pinsManagerService)
        {
            _userDialogs = userDialogs;
        }

        #region -- Commands --

        private ICommand _toAddPinPageCommand;
        public ICommand ToAddPinPageCommand => _toAddPinPageCommand ??= new Command(OnToAddPinPageCommandAsync);

        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ??= new Command<CustomPin>(OnDeleteCommandAsync);

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ??= new Command<CustomPin>(OnEditCommandAsync);

        private ICommand _pinTappedCommand;
        public ICommand PinTappedCommand => _pinTappedCommand ??= new Command<CustomPin>(OnPinTappedCommandAsync);

        private ICommand _favouriteChangeCommand;
        public ICommand FavouriteChangeCommand => _favouriteChangeCommand ??= new Command<CustomPin>(OnFavouriteChangeCommandAsync);

        #endregion

        #region -- Command Methods --

        private async void OnFavouriteChangeCommandAsync(CustomPin pin)
        {
            await ChangePinIsFavoriteAsync(pin);
        }

        private async void OnPinTappedCommandAsync(CustomPin pin)
        {
            if (!pin.IsFavorite)
            {
                await ChangePinIsFavoriteAsync(pin);
            }

            NavigationParameters navParams = new NavigationParameters
            {
                { nameof(CustomPin), pin }
            };

            #region -- Navigaiton Hack --

            var currentPage = (NavigationService as IPageAware).Page;

            if (currentPage is TabbedPage tp)
            {
                var page = tp.Children.First(x => x.BindingContext.Equals(this));

                page.Parent = currentPage;
                (NavigationService as IPageAware).Page = page;
            }

            #endregion

            await NavigationService.SelectTabAsync($"{nameof(MapPage)}", navParams);
        }

        private async void OnToAddPinPageCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinPage)}");
        }

        private async void OnDeleteCommandAsync(CustomPin pin)
        {
            bool result = await _userDialogs.ConfirmAsync(Resources["SureQuestion"]);
            if (result)
            {
                await DeletePinAsync(pin);
            }
        }

        private async void OnEditCommandAsync(CustomPin pin)
        {
            NavigationParameters navParams = new NavigationParameters
            {
                { nameof(CustomPin), pin }
            };                                             
            await NavigationService.NavigateAsync(nameof(AddPinPage), navParams);
        }

        #endregion

        #region -- Private Helpers --

        private async Task ChangePinIsFavoriteAsync(CustomPin pin)
        {
            if (pin != null)
            {
                pin.IsFavorite = !pin.IsFavorite;
                await SavePinAsync(pin);
            }
        }

        #endregion
    }
}
