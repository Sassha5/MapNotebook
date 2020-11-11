using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Views
{
    public partial class MapPage : BaseContentPage
    {
        private int moveDistance = 500;

        public MapPage()
        {
            InitializeComponent();
            pinInfo.TranslationY = moveDistance;
        }

        public void MapClicked(object sender, MapClickedEventArgs args)
        {
            if (pinInfo.TranslationY <= 0)
            {
                pinInfo.TranslateTo(0, pinInfo.TranslationY + moveDistance, 500);
            }
        }

        public void PinClicked(object sender, PinClickedEventArgs args)
        {
            if (pinInfo.TranslationY > 0)
            {
                pinInfo.TranslateTo(0, pinInfo.TranslationY - moveDistance, 500);
            }
        }
    }
}
