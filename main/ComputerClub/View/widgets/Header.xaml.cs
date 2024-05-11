using ComputerClub.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.View.widgets
{
    public partial class Header : UserControl
    {
        public HeaderVM ViewModel { get;}

        public Header()
        {
            InitializeComponent();
            DataContext = ViewModel = new HeaderVM();

            GeoIcon.OnSelectedItemChanged += ViewModel.SwitchSelectedClub;
        }


        public static readonly DependencyProperty SectionTextProperty =
             DependencyProperty.Register(nameof(SectionText), typeof(string), typeof(Header));

        public string SectionText
        {
            get => (string)GetValue(SectionTextProperty);
            set => SetValue(SectionTextProperty, value);
        }
    }
}
