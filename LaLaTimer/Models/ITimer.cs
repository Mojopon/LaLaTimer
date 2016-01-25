using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLaTimer.Models
{
    public enum TimerPhase
    {
        IsIdle,
        IsRunning,
        IsStopped,
    }

    public interface ITimer
    {
        string Name { get; set; }
        int Hour { get; }
        int Minute { get; }
        int Second { get; }

        ReactiveProperty<bool> CountdownEnd { get; }
        ReactiveProperty<TimerPhase> Phase { get; }
        ReactiveProperty<double> Progress { get; }

        void Start();
        void Stop();
        void Reset();
    }
}
