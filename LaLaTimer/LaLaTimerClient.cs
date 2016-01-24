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
            CreateNewTimer(timer);
        }

        public void CreateNewTimer(ITimer timer)
        {
            _Timers.Add(timer);
            TimerGateway.OnNext(timer);
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
