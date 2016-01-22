using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLaTimer.Models
{
    public class TimerTime
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public TimerTime(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }
    }

    public class PomodoroTimer : TimerBase
    {
        private TimerTime taskTime;
        private TimerTime breakTime;
        private TimerTime longBreakTime;

        public override ReactiveProperty<double> Progress { get; } = new ReactiveProperty<double>();

        public int RepeatTime;
        public ReactiveProperty<int> RepeatTimeLeft = new ReactiveProperty<int>();
        private TimerTime current;
        public PomodoroTimer(TimerTime taskTime, TimerTime breakTime, int repeat, TimerTime longBreakTime) : base()
        {
            this.taskTime = taskTime;
            this.breakTime = breakTime;
            this.RepeatTime = repeat;
            this.longBreakTime = longBreakTime;

            Reset();

            CountdownEnd.Subscribe(x =>
            {
                if (x)
                {
                    SwitchTimer();
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
            current = taskTime;
            RepeatTimeLeft.Value = RepeatTime;

            ResetToDestination(current);
        }

        void SwitchTimer()
        {
            if (current == taskTime)
            {
                RepeatTimeLeft.Value--;
                if (RepeatTimeLeft.Value <= 0)
                {
                    current = longBreakTime;
                }
                else
                {
                    current = breakTime;
                }
            }
            else if (current == breakTime)
            {
                current = taskTime;
            }
            else if (current == longBreakTime)
            {
                Stop();
                Reset();
                Phase.Value = TimerPhase.IsIdle;

                return;
            }

            Stop();
            ResetToDestination(current);
        }

        protected override void TimerEnd()
        {
            CountdownEnd.Value = true;
        }

        void ResetToDestination(TimerTime destination)
        {
            Hour = destination.Hour;
            Minute = destination.Minute;
            Second = destination.Second;
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
            return current.Second + current.Minute * 60 + ((current.Hour * 60) * 60);
        }

        private double GetTotalCurrentTimeSecond()
        {
            return Second + Minute * 60 + ((Hour * 60) * 60);
        }
    }
}
