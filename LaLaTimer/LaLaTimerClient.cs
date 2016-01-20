using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using LaLaTimer.Models;

namespace LaLaTimer
{
    public class LaLaTimerClient
    {
        public static LaLaTimerClient Current { get; } = new LaLaTimerClient();

        private BehaviorSubject<ITimer> TimerGateway = new BehaviorSubject<ITimer>(new PomodoroTimer(
            new TimerTime(0, 2, 0),
            new TimerTime(0, 1, 0),
            3,
            new TimerTime(0, 3, 0)
            ));
        public IObservable<ITimer> OnChangeTimer => this.TimerGateway.AsObservable();


    }
}
