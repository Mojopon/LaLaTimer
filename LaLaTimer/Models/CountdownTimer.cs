using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LaLaTimer.Utility;

using Livet;
using Reactive.Bindings;

namespace LaLaTimer.Models
{
    public class CountdownTimer : TimerBase
    {
        private int startHour;
        private int startMinute;
        private int startSecond;

        public double Progress { get { return GetProgress(); } }

        public CountdownTimer(int startHour, int startMinute, int startSecond) : base()
        {
            this.startHour = startHour;
            this.startMinute = startMinute;
            this.startSecond = startSecond;

            Reset();

            this.CountdownEnd.Subscribe(x =>
            {
                if (x)
                {
                    Stop();
                    Reset();
                }
            });
        }

        public override void Reset()
        {
            Hour = startHour;
            Minute = startMinute;
            Second = startSecond;
            CountdownEnd.Value = false;
        }

        private double GetProgress()
        {
            var currentProgress = 1 - (GetTotalCurrentTimeSecond() / GetTotalStartTimeSecond());
            if (currentProgress > 1) currentProgress = 1;
            if (currentProgress < 0) currentProgress = 0;
            return currentProgress;
        }

        private double GetTotalStartTimeSecond()
        {
            return startSecond + startMinute * 60 + ((startHour * 60) * 60);
        }

        private double GetTotalCurrentTimeSecond()
        {
            return Second + Minute * 60 + ((Hour * 60) * 60);
        }
    }
}
