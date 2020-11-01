using System;
using MapNotepad.Services.PinsManagerService;
using Prism.Navigation;

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
    }
}
