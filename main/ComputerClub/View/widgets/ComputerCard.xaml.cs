using ComputerClub.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using ComputerClub.Repositories;


namespace ComputerClub.View.widgets
{
    public partial class ComputerCard : UserControl
    {
        public ComputerCard()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty ComputerNumberProperty =
            DependencyProperty.Register(nameof(Number), typeof(int), typeof(ComputerCard));

        public static readonly DependencyProperty RateNameProperty =
            DependencyProperty.Register(nameof(RateName), typeof(string), typeof(ComputerCard));

        public static readonly DependencyProperty RentProperty =
            DependencyProperty.Register(nameof(Rent), typeof(Rent), typeof(ComputerCard));

        public int Number
        {
            get => (int)GetValue(ComputerNumberProperty);
            set => SetValue(ComputerNumberProperty, value);
        }

        public string RateName
        {
            get => (string)GetValue(RateNameProperty);
            set => SetValue(RateNameProperty, value);
        }

        public Rent Rent
        {
            get => (Rent)GetValue(RentProperty);
            set => SetValue(RentProperty, value);
        }

        public string Status => TimeLeft == "" ? "свободен" : "занят";

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


                //rent is over
                RentsRepository repository = RepositoryServiceLocator.Resolve<RentsRepository>();
                Rent oldRent = Rent;
                Rent.Computer.Rent = null;
                repository.Delete(oldRent);

                Logger.Add(oldRent.ToString() + " окончена!");

                return "00:00:00";
            }
        }
    }
}
