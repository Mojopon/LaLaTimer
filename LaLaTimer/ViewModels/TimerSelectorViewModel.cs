﻿using System;
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

namespace LaLaTimer.ViewModels
{
    public class TimerSelectorViewModel : ViewModel
    {
        public ObservableCollection<ITimer> Timers { get; set; }

        public TimerSelectorViewModel()
        {
            Timers = LaLaTimerClient.Current.Timers;
        }

        public void Initialize()
        {
        }
    }
}