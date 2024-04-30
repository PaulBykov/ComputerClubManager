using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.View.widgets
{

    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty SectionTextProperty =
             DependencyProperty.Register("SectionText", typeof(string), typeof(Header));

        public string SectionText
        {
            get { return (string)GetValue(SectionTextProperty); }
            set { SetValue(SectionTextProperty, value); }
        }
    }
}
