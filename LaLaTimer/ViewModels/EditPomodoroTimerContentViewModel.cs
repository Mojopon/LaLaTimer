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

namespace LaLaTimer.ViewModels
{
    public class EditPomodoroTimerContentViewModel : ViewModel
    {

        #region Timer変更通知プロパティ
        private PomodoroTimer _Timer;

        public PomodoroTimer Timer
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

        public EditPomodoroTimerContentViewModel(PomodoroTimer timer)
        {
            Timer = timer;
        }

        public void Initialize()
        {
        }
    }
}
