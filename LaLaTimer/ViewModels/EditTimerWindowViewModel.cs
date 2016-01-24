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


        public EditTimerWindowViewModel()
        {
            CompositeDisposable = new LivetCompositeDisposable();
        }

        public void Initialize()
        {
            LaLaTimerClient.Current.Timer.Subscribe(x =>
            {
                var type = x.GetType();

                if (type == typeof(CountdownTimer))
                {
                    Content = new EditCountdownTimerContentViewModel();
                }
                else if (type == typeof(PomodoroTimer))
                {
                    Content = new EditPomodoroTimerContentViewModel();
                }
            }).AddTo(CompositeDisposable);
        }
    }
}
