using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Behaviors
{
    public class MapBehavior : Behavior<Map>
    {
        public MapBehavior()
        {
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(MapBehavior), null);
        public static readonly BindableProperty InputConverterProperty =
                BindableProperty.Create("Converter", typeof(IValueConverter), typeof(MapBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        void OnMapClicked(object sender, SelectedItemChangedEventArgs e)
        {
            if (Command == null)
            {
                return;
            }

            object parameter = Converter.Convert(e, typeof(object), null, null);
            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }
    }
}
