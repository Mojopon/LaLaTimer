using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LaLaTimer.Models;
using LaLaTimer.Editor;
using Reactive.Bindings;

namespace LaLaTimer.ViewModels
{
    public class TimerTypeControlViewModel : ViewModel
    {
        public List<TimerType> TimerTypes { get; } = new List<TimerType>() { TimerType.CountdownTimer, TimerType.PomodoroTimer };
        public ReactiveProperty<TimerType> SelectedTimerType { get; private set; } = new ReactiveProperty<TimerType>();

        public TimerTypeControlViewModel()
        {
            CompositeDisposable = new LivetCompositeDisposable();

            SelectedTimerType.Value = LaLaTimerEditor.Current.CurrentTimer.TimerType;

            SelectedTimerType.Subscribe(x =>
            {
                if(x != LaLaTimerEditor.Current.CurrentTimer.TimerType)
                {
                    Console.WriteLine("Timer type changed: " + x);
                    LaLaTimerEditor.Current.Recreate(LaLaTimerEditor.Current.CurrentTimer, x);
                }
            });
        }

        public void Initialize()
        {
        }
    }
}
