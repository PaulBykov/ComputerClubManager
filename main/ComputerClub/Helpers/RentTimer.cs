using System;
using System.Windows.Threading;

namespace ComputerClub.Model
{
    public class RentTimer
    {
        private DispatcherTimer _timer;

        private Action _handler;
        public RentTimer(Action handler, int intervalSeconds = 1) 
        {
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Interval = new TimeSpan(0, 0, 0, intervalSeconds);

            _handler = handler;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop() 
        {
            _timer.Stop(); 
        }

        private void TimerTick(object sender, EventArgs args)
        {
            _handler?.Invoke();
        }

        ~RentTimer()
        {
            _timer.Stop();
            _timer = null;
            _handler = null;
        }
    }
}
