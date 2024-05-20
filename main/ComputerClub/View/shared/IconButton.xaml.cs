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
            DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(IconButton));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(IconButton));

        public static readonly DependencyProperty WidthDependencyProperty =
            DependencyProperty.Register(nameof(Width), typeof(int), typeof(IconButton));

        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(IconButton), new PropertyMetadata(null));

        public static readonly DependencyProperty NamePropery =
            DependencyProperty.Register(nameof(Name), typeof(string), typeof(IconButton));
        
        public static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register(
                nameof(Visibility),
                typeof(Visibility),
                typeof(IconButton),
                new PropertyMetadata(Visibility.Visible, OnVisibilityChanged));

        public new Visibility Visibility
        {
            get { return (Visibility)GetValue(VisibilityProperty); }
            set { SetValue(VisibilityProperty, value); }
        }

        public new string Name
        {
            get => (string)GetValue(NamePropery);
            set => SetValue(NamePropery, value);
        }

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

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }


        private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IconButton control = d as IconButton;
            if (control != null)
            {
                if ((Visibility)e.NewValue != Visibility.Visible)
                {
                    control.Height = 0;
                }
            }
        }
    }
}
