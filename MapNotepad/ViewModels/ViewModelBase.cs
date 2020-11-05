using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MapNotepad.Localization;
using MapNotepad.Resources;
using Prism.Mvvm;
using Prism.Navigation;

namespace MapNotepad.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IInitialize, INavigatedAware, IDestructible
    {
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Resources = new LocalizedResources(typeof(AppResource), "en"); //TODO take language from settings, not hardcoded "en"
        }

        #region Properties

        public LocalizedResources Resources { get; private set; }

        protected INavigationService NavigationService { get; private set; }

        #endregion

        #region Interfaces implementation

        public virtual void Initialize(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual Task OnNavigatedToAsync(INavigationParameters parameters) => Task.FromResult(true);

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            OnNavigatedToAsync(parameters);
        }

        public virtual void Destroy()
        {

        }

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
