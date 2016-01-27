using LaLaTimer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace LaLaTimer.Editor
{
    public class LaLaTimerEditor
    {
        public static LaLaTimerEditor Current { get; } = new LaLaTimerEditor();

        public BehaviorSubject<ITimer> TimerGateway = new BehaviorSubject<ITimer>(null);
        public IObservable<ITimer> Timer => this.TimerGateway.AsObservable();
        public ITimer CurrentTimer;

        private ObservableCollection<ITimer> Timers = LaLaTimerClient.Current.Timers;

        private LaLaTimerEditor()
        {
            TimerGateway.Subscribe((timer) => CurrentTimer = timer);
        }

        public void Initialize()
        {

        }

        public void Edit(ITimer timer)
        {
            TimerGateway.OnNext(timer);
        }

        public void Recreate(ITimer target, TimerType type)
        {
            var index = LaLaTimerClient.Current.GetIndex(target);

            var newTimer = Create(type);
            newTimer.Name = target.Name;
            Timers[index] = newTimer;
            Edit(newTimer);
        }

        public ITimer CreateNew(TimerType timerType)
        {
            var newTimer = Create(timerType);
            LaLaTimerClient.Current.Add(newTimer);
            return newTimer;
        }

        private ITimer Create(TimerType timerType)
        {
            ITimer newTimer = null;
            switch(timerType)
            {
                case TimerType.CountdownTimer:
                    newTimer = new CountdownTimer();
                    break;
                case TimerType.PomodoroTimer:
                    newTimer = new PomodoroTimer();
                    break;
            }

            return newTimer;
        }
    }
}
