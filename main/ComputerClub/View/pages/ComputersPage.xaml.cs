using ComputerClub.Model;
using ComputerClub.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;


namespace ComputerClub.View.pages
{
    public partial class ComputersPage : Page
    {
        private ObservableCollection<Computer> _computers;
        private DispatcherTimer _timer;

        public ComputersPage()
        {
            InitializeComponent();
            DataContext = this;

            ComputerClubContext _context = new ComputerClubContext();
            _context.Rents.Load();
            _computers = new ObservableCollection<Computer>(_context.Computers);

            timerStart();
        }

        public ObservableCollection<Computer> Computers
        {
            get { return _computers;}
        }

        private void timerStart()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Render); 
            _timer.Tick += new EventHandler(timerTick);
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            ListViewComputers.Items.Refresh();
        }
    }
}
