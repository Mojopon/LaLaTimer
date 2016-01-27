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
using System.Collections.ObjectModel;
using Reactive.Bindings;
using System.Reactive.Linq;
using Reactive.Bindings.Extensions;

namespace LaLaTimer.ViewModels
{
    public class TimerSelectorViewModel : ViewModel
    {
        public ReactiveProperty<ITimer> SelectedTimer { get; private set; } = new ReactiveProperty<ITimer>();

        public ObservableCollection<ITimer> Timers { get; set; }

        public TimerSelectorViewModel()
        {
            CompositeDisposable = new LivetCompositeDisposable();
            Timers = LaLaTimerClient.Current.Timers;

            LaLaTimerClient.Current.Timer.Subscribe((timer) => SelectedTimer.Value = timer);
        }

        public void Initialize()
        {
        }
    }
}
