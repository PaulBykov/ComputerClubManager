using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.View.widgets
{
    public partial class ComputerCard : UserControl
    {
        public ComputerCard()
        {
            InitializeComponent();
            DataContext = this;
        }


        public static readonly DependencyProperty ComputerNumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(ComputerCard));

        public static readonly DependencyProperty RateNameProperty =
            DependencyProperty.Register("RateName", typeof(string), typeof(ComputerCard));

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(ComputerCard));

        public static readonly DependencyProperty TimeLeftProperty =
            DependencyProperty.Register("TimeLeft", typeof(string), typeof(ComputerCard));


        public int Number
        {
            get { return (int)GetValue(ComputerNumberProperty); }
            set { SetValue(ComputerNumberProperty, value); }
        }

        public string RateName
        {
            get { return (string)GetValue(RateNameProperty); }
            set { SetValue(RateNameProperty, value); }
        }

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public string TimeLeft
        {
            get { return (string)GetValue(TimeLeftProperty); }
            set { SetValue(TimeLeftProperty, value); }
        }
    }
}
