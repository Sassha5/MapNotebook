using System;
using Prism.Behaviors;
using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;

namespace MapNotepad.Behaviors
{
    public class TabbedPageNavigationBehavior : BehaviorBase<TabbedPage>
    {
        private Page CurrentPage;

        #region -- Protected Implementation --

        protected override void OnAttachedTo(TabbedPage bindable)
        {
            bindable.CurrentPageChanged += OnCurrentPageChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            bindable.CurrentPageChanged -= OnCurrentPageChanged;
            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region -- Private Helpers --

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            var newPage = AssociatedObject.CurrentPage;

            if (CurrentPage != null)
            {
                var parameters = new NavigationParameters();
                PageUtilities.OnNavigatedFrom(CurrentPage, parameters);
                PageUtilities.OnNavigatedTo(newPage, parameters);
            }

            CurrentPage = newPage;
        }

        #endregion
    }
}
