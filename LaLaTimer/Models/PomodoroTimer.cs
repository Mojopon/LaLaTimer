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
        public enum PomodoroPhase
        {
            Task,
            Break,
            LongBreak,
        }

        private TimerTime taskTime;
        private TimerTime breakTime;
        private TimerTime longBreakTime;

        public int RepeatTime;
        public ReactiveProperty<PomodoroPhase> Phase = new ReactiveProperty<PomodoroPhase>();
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
                if(x)
                {
                    SwitchTimer();
                }
            });
        }

        public override void Reset()
        {
            current = taskTime;
            RepeatTimeLeft.Value = RepeatTime;
            Phase.Value = PomodoroPhase.Task;

            ResetToDestination(current);
        }

        void SwitchTimer()
        {
            switch(Phase.Value)
            {
                case PomodoroPhase.Task:
                    {
                        RepeatTimeLeft.Value--;
                        if(RepeatTimeLeft.Value <= 0)
                        {
                            current = longBreakTime;
                            Phase.Value = PomodoroPhase.LongBreak;
                        }else
                        {
                            current = breakTime;
                            Phase.Value = PomodoroPhase.Break;
                        }
                    }
                    break;
                case PomodoroPhase.Break:
                    {
                        current = taskTime;
                        Phase.Value = PomodoroPhase.Task;
                    }
                    break;
                case PomodoroPhase.LongBreak:
                    {
                        Reset();
                        Stop();
                        return;
                    }
            }

            ResetToDestination(current);
            Stop();
        }

        void ResetToDestination(TimerTime destination)
        {
            Hour = destination.Hour;
            Minute = destination.Minute;
            Second = destination.Second;
        }
    }
}
