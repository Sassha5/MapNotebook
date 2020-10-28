using System;
using MapNotepad.Services.AuthorizationService;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.RegistrationService;
using MapNotepad.Services.RepositoryService;
using MapNotepad.Services.SettingsManagerService;
using MapNotepad.Services.UsersManagerService;
using MapNotepad.Services.ValidationService;
using MapNotepad.ViewModels;
using MapNotepad.Views;
using Plugin.Settings;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace MapNotepad
{
    public partial class App : PrismApplication
    {
        private ISettingsManagerService _settingsManager;
        private ISettingsManagerService SettingsManager =>
            _settingsManager ??= Container.Resolve<ISettingsManagerService>();

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        protected override async void OnInitialized()
        {
            Device.SetFlags(new string[] { "AppTheme_Experimental" });

            InitializeComponent();

            Application.Current.UserAppTheme = (OSAppTheme)SettingsManager.Theme;

            string path = nameof(SignInPage);
            if (SettingsManager.UserLoggedIn)
            {
                path = nameof(Views.MainPage);
            }
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{path}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<PinsPage, PinsPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();

            containerRegistry.RegisterInstance(CrossSettings.Current);

            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());
            containerRegistry.RegisterInstance<IRepositoryService>(Container.Resolve<RepositoryService>());
            containerRegistry.RegisterInstance<IPinsManagerService>(Container.Resolve<PinsManagerService>());
            containerRegistry.RegisterInstance<IUsersManagerService>(Container.Resolve<UsersManagerService>());
            containerRegistry.RegisterInstance<IRegistrationService>(Container.Resolve<RegistrationService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IValidationService>(Container.Resolve<ValidationService>());
        }
    }
}
