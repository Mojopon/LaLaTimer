using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using LaLaTimer.Models;
using System.Collections.ObjectModel;

namespace LaLaTimer
{
    public class LaLaTimerClient
    {
        public static LaLaTimerClient Current { get; } = new LaLaTimerClient();

        private BehaviorSubject<ITimer> TimerGateway = new BehaviorSubject<ITimer>(new CountdownTimer(0, 5, 0));
        public IObservable<ITimer> Timer => this.TimerGateway.AsObservable();

        private ObservableCollection<ITimer> _Timers = new ObservableCollection<ITimer>();
        public ObservableCollection<ITimer> Timers { get { return _Timers; } }

        public LaLaTimerClient()
        {
            var timer = new PomodoroTimer(
                            new TimerTime(0, 2, 0),
                            new TimerTime(0, 1, 0),
                            2,
                            new TimerTime(0, 3, 0)
                );
            AddTimer(timer);
            var timer2 = new CountdownTimer(0, 5, 0);
            AddTimer(timer2);
            SelectTimer(timer);
            SelectTimer(timer2);
        }

        public void AddTimer(ITimer timer)
        {
            _Timers.Add(timer);
            if (string.IsNullOrEmpty(timer.Name))
            {
                timer.Name = "Timer " + _Timers.Count;
            }
        }

        public void SelectTimer(ITimer timer, bool resetTime)
        {
            if (Timers.Count == 0 || !Timers.Contains(timer)) return;

            if (resetTime)
            {
                timer.Reset();
            }
            TimerGateway.OnNext(Timers[TimerIndex(timer)]);
        }

        public void SelectTimer(ITimer timer)
        {
            SelectTimer(timer, false);
        }

        public int TimerIndex(ITimer timer)
        {
            int index = 0;
            foreach(var item in Timers)
            {
                if (timer == item) break;
                index++;
            }
            return index;
        }
    }
}
