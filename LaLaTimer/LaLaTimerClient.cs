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

        private BehaviorSubject<CountdownTimer> TimerGateway = new BehaviorSubject<CountdownTimer>(new CountdownTimer(0, 5, 0));
        public IObservable<CountdownTimer> OnChangeTimer => this.TimerGateway.AsObservable();


    }
}
