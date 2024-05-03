using Microsoft.VisualBasic;
using System;
using System.Linq.Expressions;
using System.Windows.Threading;

namespace ComputerClub.Model
{
    public class ComputerTimer
    {
        private DispatcherTimer _timer;

        private LambdaExpression _handler;
        public ComputerTimer(LambdaExpression handler, int intervalSeconds = 1) 
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
            _handler.Compile();
        }
    }
}
