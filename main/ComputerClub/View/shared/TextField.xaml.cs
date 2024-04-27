using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.View.shared
{
    public partial class TextField : UserControl
    {
        public TextField()
        {
            InitializeComponent();
            DataContext = this;
        }


        public static readonly DependencyProperty PlaceholderProperty =
          DependencyProperty.Register("PlaceHolder", typeof(string), typeof(TextField));

        public static readonly DependencyProperty WidthDependencyProperty =
          DependencyProperty.Register("Width", typeof(int), typeof(TextField));


        public new int Width
        {
            get { return (int)GetValue(WidthDependencyProperty); }
            set { SetValue(WidthDependencyProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

    }
}
