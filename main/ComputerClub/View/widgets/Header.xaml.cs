using ComputerClub.Services;
using ComputerClub.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.View.widgets
{

    public partial class Header : UserControl
    {
        private HeaderVM ViewModel { get; set; }

        public Header()
        {
            InitializeComponent();
            DataContext = ViewModel = new HeaderVM();

            UserIcon.ItemsSource = ViewModel.UserButtons;

            int clubId = AuthService.GetInstance().CurrentClub.Id;
            GeoIcon.ItemsSource = ViewModel.Clubs;
            GeoIcon.SelectedItem = ViewModel.Clubs.Where(c => c.Id == clubId).First();
            GeoIcon.OnSelectedItemChanged += ViewModel.SwitchSelectedClub;
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
