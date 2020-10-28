using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MapNotepad.Localization;
using Prism.Mvvm;
using Prism.Navigation;

namespace MapNotepad.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigatedAware, IDestructible
    {
        public LocalizedResources Resources
        {
            get;
            private set;
        }
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            //Resources = new LocalizedResources(typeof(AppResource), SettingsManager.Language);
        }

        public virtual void Initialize(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual void Destroy() { }

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
