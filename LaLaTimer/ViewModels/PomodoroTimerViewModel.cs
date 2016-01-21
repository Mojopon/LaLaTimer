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
using Reactive.Bindings;
using System.Collections.ObjectModel;
using Reactive.Bindings.Extensions;

namespace LaLaTimer.ViewModels
{
    public class PomodoroTimerViewModel : TimerViewModelBase
    {

        #region RepeatTimeLeft変更通知プロパティ
        private List<int> _RepeatTimeLeft;

        public List<int> RepeatTimeLeft
        {
            get
            { return _RepeatTimeLeft; }
            set
            { 
                if (_RepeatTimeLeft == value)
                    return;
                _RepeatTimeLeft = value;
                RaisePropertyChanged();
            }
        }
        #endregion



        protected override void OnChangeTimer(ITimer timer)
        {
            base.OnChangeTimer(timer);

            var pomodoroTimer = timer as PomodoroTimer;
            if (pomodoroTimer == null) return;

            pomodoroTimer.RepeatTimeLeft.Subscribe(x =>
            {
                RepeatTimeLeft = new int[x].ToList();
            }).AddTo(CompositeDisposable);
        }
    }
}
