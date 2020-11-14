using Xamarin.Forms;

namespace MapNotepad.Controls
{
    public partial class CustomEntry : Frame
    {
        public CustomEntry()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                propertyName: nameof(Text),
                returnType: typeof(string),
                declaringType: typeof(CustomEntry),
                defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                propertyName: nameof(Placeholder),
                returnType: typeof(string),
                declaringType: typeof(CustomEntry),
                defaultBindingMode: BindingMode.TwoWay);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(
                propertyName: nameof(IsPassword),
                returnType: typeof(bool),
                declaringType: typeof(CustomEntry),
                defaultBindingMode: BindingMode.TwoWay);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty =
            BindableProperty.Create(
                propertyName: nameof(IsReadOnly),
                returnType: typeof(bool),
                declaringType: typeof(CustomEntry),
                defaultBindingMode: BindingMode.TwoWay);

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(
                propertyName: nameof(Keyboard),
                returnType: typeof(Keyboard),
                declaringType: typeof(CustomEntry),
                defaultBindingMode: BindingMode.TwoWay);

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }
    }
}
