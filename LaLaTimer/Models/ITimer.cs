﻿using Reactive.Bindings;
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
        int Hour { get; }
        int Minute { get; }
        int Second { get; }

        ReactiveProperty<bool> CountdownEnd { get; }
        ReactiveProperty<TimerPhase> Phase { get; }

        void Start();
        void Stop();
    }
}
