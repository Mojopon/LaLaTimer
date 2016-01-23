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


        #region Hour変更通知プロパティ
        private int _Hour;

        public int Hour
        {
            get
            { return _Hour; }
            set
            { 
                if (_Hour == value)
                    return;
                _Hour = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region Minute変更通知プロパティ
        private int _Minute;

        public int Minute
        {
            get
            { return _Minute; }
            set
            { 
                if (_Minute == value)
                    return;
                _Minute = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region Second変更通知プロパティ
        private int _Second;

        public int Second
        {
            get
            { return _Second; }
            set
            { 
                if (_Second == value)
                    return;
                _Second = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public EditCountdownTimerContentViewModel()
        {
            LaLaTimerClient.Current.OnChangeTimer.Subscribe(OnChangeTimer);
            Hour = 1;
            Minute = 20;
            Second = 30;
        }

        void OnChangeTimer(ITimer timer)
        {
            Timer = timer as CountdownTimer;
            if (Timer == null) return;

            InitialTime = Timer.InitialTime;
            Console.WriteLine("Initial time has been set");
        }

        public void Initialize()
        {
        }
    }
}
