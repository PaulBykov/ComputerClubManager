using ComputerClub.Services;
using ComputerClub.ViewModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComputerClub.View
{

    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = ViewModel = new HomePageVM();
            AuthService.GetInstance().PropertyChanged += HandlePropertyChanges;
        }

        private HomePageVM ViewModel { get; set; }
        
        public Color OpacityColor {get;} = Color.FromArgb(176, 0, 0, 100);


        private void HandlePropertyChanges(object sender, PropertyChangedEventArgs e) 
        {
            ViewModel.UpdateData();
        }
    }
}
