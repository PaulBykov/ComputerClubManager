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
           DependencyProperty.Register("Width", typeof(int), typeof(SubmitButton));

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text", typeof(string), typeof(SubmitButton));

        public static readonly DependencyProperty IconSourceProperty =
           DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(SubmitButton));

        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register("Command", typeof(ICommand), typeof(SubmitButton), new PropertyMetadata(null));
        

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public new int Width
        {
            get { return (int)GetValue(WidthDependencyProperty); }
            set { SetValue(WidthDependencyProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

    }
}
