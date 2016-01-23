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
        public TimerTime InitialTime { get; private set; }

        public override ReactiveProperty<double> Progress { get; } = new ReactiveProperty<double>();

        public CountdownTimer(TimerTime initialTime)
        {

        }

        public CountdownTimer(int initialHour, int initialMinute, int initialSecond) : base()
        {
            InitialTime = new TimerTime(initialHour, initialMinute, initialSecond);

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

        public override void Tick()
        {
            base.Tick();
            Progress.Value = GetProgress();
        }

        public override void Reset()
        {
            Hour = InitialTime.Hour;
            Minute = InitialTime.Minute;
            Second = InitialTime.Second;
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
            return InitialTime.Second + InitialTime.Minute * 60 + ((InitialTime.Hour * 60) * 60);
        }

        private double GetTotalCurrentTimeSecond()
        {
            return Second + Minute * 60 + ((Hour * 60) * 60);
        }
    }
}
