using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LaLaTimer.Utility;

using Livet;
using Reactive.Bindings;

namespace LaLaTimer.Models
{
    public class CountdownTimer : NotificationObject
    {
        private int startHour;
        private int startMinute;
        private int startSecond;

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

        public ReactiveProperty<bool> IsRunning { get; private set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> CountdownEnd { get; private set; } = new ReactiveProperty<bool>(false);

        public double Progress { get { return GetProgress(); } }

        public CountdownTimer(int startHour, int startMinute, int startSecond)
        {
            timer = new DispatcherTimerManager(250);
            timer.OnTick += (() => Tick());

            this.startHour = startHour;
            this.startMinute = startMinute;
            this.startSecond = startSecond;
            Reset();
        }

        private DispatcherTimerManager timer;
        public void Start()
        {
            timer.Start();
            IsRunning.Value = true;
        }

        public void Stop()
        {
            timer.Stop();
            IsRunning.Value = false;
        }

        public void Tick()
        {
            if (CountdownEnd.Value) return;
            ProgressSecond();
        }

        public void Reset()
        {
            Hour = startHour;
            Minute = startMinute;
            Second = startSecond;
            CountdownEnd.Value = false;
        }

        private void ProgressSecond()
        {
            if (Second <= 0)
            {
                ProgressMinute();
                if (CountdownEnd.Value) return;
                Second = 60 + Second;
            }

            Second--;
        }

        private void ProgressMinute()
        {
            if (Minute <= 0)
            {
                ProgressHour();
                if (CountdownEnd.Value) return;
                Minute = 60 + Minute;
            }
            Minute--;

            return;
        }

        private void ProgressHour()
        {
            if(Hour <= 0)
            {
                CountdownEnd.Value = true;
                return;
            }

            Hour--;
            return;
        }

        private double GetProgress()
        {
            var currentProgress = 1 - (GetTotalCurrentTimeSecond() / GetTotalStartTimeSecond());
            if (currentProgress > 1) currentProgress = 1;
            if (currentProgress < 0) currentProgress = 0;
            return currentProgress;
        }

        private double GetTotalStartTimeSecond()
        {
            return startSecond + startMinute * 60 + ((startHour * 60) * 60);
        }

        private double GetTotalCurrentTimeSecond()
        {
            return Second + Minute * 60 + ((Hour * 60) * 60);
        }
    }
}
