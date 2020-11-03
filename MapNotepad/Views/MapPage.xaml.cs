using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Views
{
    public partial class MapPage : BaseContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            pinInfo.TranslationY = 300;
        }

        public void MapClicked(object sender, MapClickedEventArgs args)
        {
            if (pinInfo.TranslationY <= 0)
            {
                pinInfo.TranslateTo(0, pinInfo.TranslationY + 300, 500);
            }
        }

        public void PinClicked(object sender, PinClickedEventArgs args)
        {
            if (pinInfo.TranslationY > 0)
            {
                pinInfo.TranslateTo(0, pinInfo.TranslationY - 300, 500);
            }
        }
    }
}
