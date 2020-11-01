using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace MapNotepad.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            Style = (Style)Application.Current.Resources["ContentPageStyle"];
        }
    }
}

