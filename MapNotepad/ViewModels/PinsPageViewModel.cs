﻿using System;
using System.Linq;
using System.Windows.Input;
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
        public PinsPageViewModel(INavigationService navigationService,
                                 IPinsManagerService pinsManagerService)
                                 : base(navigationService,
                                        pinsManagerService)
        {
        }

        #region Commands

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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        #region Command execution methods

        private async void OnFavouriteChangeCommandAsync(CustomPin pin)
        {
            if (pin != null)
            {
                pin.IsFavorite = !pin.IsFavorite;
                await SavePinAsync(pin);
            }
        }

        private async void OnPinTappedCommandAsync(CustomPin pin)
        {
            pin.IsFavorite = true;
            await SavePinAsync(pin);

            NavigationParameters navParams = new NavigationParameters
            {
                { nameof(CustomPin), pin }
            };

            #region -- Secret MAGIC --

            var currentPage = (NavigationService as IPageAware).Page;

            if (currentPage is TabbedPage tp)
            {
                var page = tp.Children.First(x => x.BindingContext.Equals(this));

                page.Parent = currentPage;
                (NavigationService as IPageAware).Page = page;
            }

            #endregion

            await NavigationService.SelectTabAsync($"{nameof(MapPage)}", navParams);
            
            //await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}?selectedTab={nameof(MapPage)}", navParams);
        }

        private async void OnToAddPinPageCommandAsync(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinPage)}");
        }

        private async void OnDeleteCommandAsync(CustomPin pin)
        {
            //bool result = await Confirm(); //TODO
            //if (result)
            await DeletePinAsync(pin);
            //}
        }

        private async void OnEditCommandAsync(CustomPin pin)
        {
            NavigationParameters navParams = new NavigationParameters
            {
                { nameof(CustomPin), pin }
            };                                              //TODO fix map to not show old pin location
            await NavigationService.NavigateAsync(nameof(AddPinPage), navParams);
        }

        #endregion
    }
}
