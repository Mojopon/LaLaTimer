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
using Reactive.Bindings.Extensions;

namespace LaLaTimer.ViewModels
{
    public class EditCountdownTimerContentViewModel : ViewModel
    {

        #region Timer変更通知プロパティ
        private CountdownTimer _Timer;

        public CountdownTimer Timer
        {
            get
            { return _Timer; }
            set
            { 
                if (_Timer == value)
                    return;
                _Timer = value;
                RaisePropertyChanged();
            }
        }
        #endregion



        #region InitialTime変更通知プロパティ
        private TimerTime _InitialTime;

        public TimerTime InitialTime
        {
            get
            { return _InitialTime; }
            set
            { 
                if (_InitialTime == value)
                    return;
                _InitialTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public EditCountdownTimerContentViewModel()
        {
            CompositeDisposable = new LivetCompositeDisposable();

            LaLaTimerEditor.Current.Timer.Subscribe(x =>
            {
                Timer = (CountdownTimer)x;
                InitialTime = Timer.InitialTime;
            }).AddTo(CompositeDisposable);

        }

        public void Initialize()
        {
        }
    }
}
