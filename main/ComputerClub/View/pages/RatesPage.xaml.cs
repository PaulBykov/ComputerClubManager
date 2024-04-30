
using ComputerClub.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace ComputerClub.View.pages
{
    public partial class RatesPage : Page
    {

        public RatesPage()
        {
            InitializeComponent();
            
            ComputerClubContext _context = new ComputerClubContext();
            _context.Computers.Load();
            Rates = new ObservableCollection<Rate>(_context.Rates);

            DataContext = this;
        }

        public ObservableCollection<Rate> Rates { get; set; }


    }
}
