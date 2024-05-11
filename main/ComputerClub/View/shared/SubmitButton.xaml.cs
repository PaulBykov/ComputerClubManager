using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ComputerClub.View.shared
{
    public partial class SubmitButton : UserControl
    {
        public SubmitButton()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WidthDependencyProperty =
           DependencyProperty.Register(nameof(Width), typeof(int), typeof(SubmitButton));

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register(nameof(Text), typeof(string), typeof(SubmitButton));

        public static readonly DependencyProperty IconSourceProperty =
           DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(SubmitButton));

        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(SubmitButton), new PropertyMetadata(null));
        

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public new int Width
        {
            get => (int)GetValue(WidthDependencyProperty);
            set => SetValue(WidthDependencyProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

    }
}
