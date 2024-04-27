using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ComputerClub.View.shared
{
    public partial class IconButton : UserControl
    {
        public IconButton()
        {
            InitializeComponent();
            DataContext = this;
        }


        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(IconButton));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IconButton));

        public static readonly DependencyProperty WidthDependencyProperty =
            DependencyProperty.Register("Width", typeof(int), typeof(IconButton));

        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register("Command", typeof(ICommand), typeof(IconButton), new PropertyMetadata(null));

        public static readonly DependencyProperty NamePropery =
            DependencyProperty.Register("Name", typeof(string), typeof(IconButton));

        public new string Name
        {
            get { return (string)GetValue(NamePropery); }
            set { SetValue(NamePropery, value); }
        }

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

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
