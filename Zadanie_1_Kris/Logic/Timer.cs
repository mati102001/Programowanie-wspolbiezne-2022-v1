using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace Logic
{
    public abstract class ATimer
    {
        public abstract event EventHandler Tick;
        
        public abstract TimeSpan Interval { get; set; }
        
        public abstract void Start();
        
        public abstract void Stop();
        
        public static ATimer CreateWPFTimer()
        {
            return new WPFTimer();
        }
        
        internal class WPFTimer : ATimer
        {
            public override event EventHandler Tick { add => timer.Tick += value; remove => timer.Tick -= value; }
            public override TimeSpan Interval { get => timer.Interval; set => timer.Interval = value; }

            public WPFTimer()
            {
                timer = new DispatcherTimer();
            }

            public override void Start()
            {
                timer.Start();
            }

            public override void Stop()
            {
                timer.Stop();
            }
            
            private DispatcherTimer timer;
        }
    }
}