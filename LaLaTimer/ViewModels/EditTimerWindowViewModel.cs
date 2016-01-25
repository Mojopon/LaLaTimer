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
using Reactive.Bindings.Extensions;

namespace LaLaTimer.ViewModels
{
    public class EditTimerWindowViewModel : ViewModel
    {

        #region Content変更通知プロパティ
        private object _Content;

        public object Content
        {
            get
            { return _Content; }
            set
            { 
                if (_Content == value)
                    return;
                _Content = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        private ITimer timer;
        public EditTimerWindowViewModel(ITimer timer)
        {
            CompositeDisposable = new LivetCompositeDisposable();

            this.timer = timer;
        }

        public void Initialize()
        {
            var type = timer.GetType();

            if (type == typeof(CountdownTimer))
            {
                Content = new EditCountdownTimerContentViewModel((CountdownTimer)timer);
            }
            else if (type == typeof(PomodoroTimer))
            {
                Content = new EditPomodoroTimerContentViewModel((PomodoroTimer)timer);
            }
        }
    }
}
