using ComputerClub.Model;
using ComputerClub.ViewModel;
using ComputerClub.ViewModel.modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerClub.View.windows
{
    public partial class EditRateModalWindow : Window, IModalWindow
    {
        public EditRateModalWindow(Rate rate)
        {
            InitializeComponent();
            DataContext = ViewModel = new EditRateWindowVM(rate);
            ViewModel.Done += Finish;
        }

        private EditRateWindowVM ViewModel { get; set; }

        public void Finish(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
