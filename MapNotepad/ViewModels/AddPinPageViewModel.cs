using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    public class AddPinPageViewModel : ViewModelBase
    {
        public AddPinPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private ICommand _AddPinCommand;
        public ICommand AddPinCommand => _AddPinCommand ??= new Command(OnAddPinCommand);

        private void OnAddPinCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
