using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLaTimer.Models
{
    public class PomodoroTimer : TimerBase
    {
        public TimerTime TaskTime { get; set; }
        public TimerTime BreakTime { get; set; }
        public int RepeatTime { get; set; }
        public TimerTime LongBreakTime { get; set; }

        public override ReactiveProperty<double> Progress { get; } = new ReactiveProperty<double>();

        public ReactiveProperty<int> RepeatTimeLeft = new ReactiveProperty<int>();
        private TimerTime current;
        public PomodoroTimer(TimerTime taskTime, TimerTime breakTime, int repeat, TimerTime longBreakTime) : base()
        {
            this.TaskTime = taskTime;
            this.BreakTime = breakTime;
            this.RepeatTime = repeat;
            this.LongBreakTime = longBreakTime;

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
            current = TaskTime;
            RepeatTimeLeft.Value = RepeatTime;

            ResetToDestination(current);
        }

        void SwitchTimer()
        {
            if (current == TaskTime)
            {
                RepeatTimeLeft.Value--;
                if (RepeatTimeLeft.Value <= 0)
                {
                    current = LongBreakTime;
                }
                else
                {
                    current = BreakTime;
                }
            }
            else if (current == BreakTime)
            {
                current = TaskTime;
            }
            else if (current == LongBreakTime)
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
