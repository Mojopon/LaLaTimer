using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using LaLaTimer.Models;
using System.Collections.ObjectModel;
using Reactive.Bindings;
using LaLaTimer.Editor;

namespace LaLaTimer
{
    public class LaLaTimerClient
    {
        public static LaLaTimerClient Current { get; } = new LaLaTimerClient();

        private BehaviorSubject<ITimer> TimerGateway = new BehaviorSubject<ITimer>(null);
        public IObservable<ITimer> Timer => this.TimerGateway.AsObservable();

        private ObservableCollection<ITimer> _Timers = new ObservableCollection<ITimer>();
        public ObservableCollection<ITimer> Timers { get { return _Timers; } }

        private LaLaTimerClient()
        {
            var timer = new PomodoroTimer(
                            new TimerTime(0, 2, 0),
                            new TimerTime(0, 1, 0),
                            2,
                            new TimerTime(0, 3, 0)
                );
            Add(timer);
            var timer2 = new CountdownTimer(0, 5, 0);
            Add(timer2);
            Select(timer);
            Select(timer2);
        }

        public void Add(ITimer timer)
        {
            Timers.Add(timer);
            if (string.IsNullOrEmpty(timer.Name))
            {
                timer.Name = "Timer " + Timers.Count;
            }
        }

        public void Select(ITimer timer, bool resetTime)
        {
            if (Timers.Count == 0 || !Timers.Contains(timer)) return;

            if (resetTime)
            {
                timer.Reset();
            }
            TimerGateway.OnNext(Timers[GetIndex(timer)]);
        }

        public void Select(ITimer timer)
        {
            Select(timer, false);
        }

        public int GetIndex(ITimer timer)
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
