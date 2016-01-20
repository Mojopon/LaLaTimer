using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LaLaTimer.Utility
{
    public class DispatcherTimerManager
    {
        public event Action OnTick;
        private bool isTicking = false;
        public bool IsTicking { get { return isTicking; } private set { isTicking = value; } }

        private DispatcherTimer dispatcherTimer;

        public DispatcherTimerManager() : this(250) { }

        public DispatcherTimerManager(int updateFrequensyMS)
        {
            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(updateFrequensyMS);
            dispatcherTimer.Tick += new EventHandler(Update);
        }

        public void Start()
        {
            IsTicking = true;
            lastUpdate = DateTime.Now;
            progress = 0;
            dispatcherTimer.Start();
            if (OnTick != null) OnTick();
        }

        public void Stop()
        {
            dispatcherTimer.Stop();
            IsTicking = false;
        }

        private DateTime lastUpdate;
        private long progress;
        void Update(object sender, EventArgs e)
        {
            progress += (long)(DateTime.Now - lastUpdate).TotalMilliseconds;
            lastUpdate = DateTime.Now;
            if (progress > 1000)
            {
                progress -= 1000;
                if (OnTick != null) OnTick();
            }
        }
    }
}
