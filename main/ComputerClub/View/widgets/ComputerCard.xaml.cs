using ComputerClub.Model;
using System;
using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.View.widgets
{
    public partial class ComputerCard : UserControl
    {
        public ComputerCard()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty ComputerNumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(ComputerCard));

        public static readonly DependencyProperty RateNameProperty =
            DependencyProperty.Register("RateName", typeof(string), typeof(ComputerCard));

        public static readonly DependencyProperty RentProperty =
            DependencyProperty.Register("Rent", typeof(Rent), typeof(ComputerCard));

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

        public Rent Rent
        {
            get { return (Rent)GetValue(RentProperty); }
            set { SetValue(RentProperty, value); }
        }

        public string Status
        {
            get { return TimeLeft == "" ? "free" : "rented" ; }
        }

        public string TimeLeft
        {
            get 
            {
                if (Rent == null)
                {
                    return "";
                }

                DateTime currentTime = DateTime.Now;
                DateTime endTime = Rent.StartTime.Add(Rent.Length.ToTimeSpan());

                if (currentTime > Rent.StartTime && currentTime < endTime)
                {
                    TimeSpan timeLeft = endTime - currentTime;
                    return timeLeft.ToString(@"hh\:mm\:ss");
                }
                else 
                {
                    return "";
                }
            }
        }
    }
}
