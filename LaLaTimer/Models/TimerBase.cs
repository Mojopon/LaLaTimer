using LaLaTimer.Utility;
using Livet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLaTimer.Models
{
    public abstract class TimerBase : NotificationObject, ITimer
    {
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

        public abstract ReactiveProperty<double> Progress { get; }
        public ReactiveProperty<bool> CountdownEnd { get; private set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<TimerPhase> Phase { get; private set; } = new ReactiveProperty<TimerPhase>(TimerPhase.IsIdle);

        private DispatcherTimerManager timer;

        public abstract void Reset();

        public TimerBase()
        {
            timer = new DispatcherTimerManager();
            timer.OnTick += (() => Tick());

            Phase.Subscribe(x => Console.WriteLine(x));
        }

        public virtual void Start()
        {
            timer.Start();
            Phase.Value = TimerPhase.IsRunning;
            CountdownEnd.Value = false;
        }

        public virtual void Stop()
        {
            timer.Stop();
            Phase.Value = TimerPhase.IsStopped;
        }

        public virtual void Tick()
        {
            if (CountdownEnd.Value) return;
            if(!ProgressSecond())
            {
                TimerEnd();
            }
        }

        private bool ProgressSecond()
        {
            if (Second <= 0)
            {
                if (!ProgressMinute()) return false;
                Second = 60 + Second;
            }

            Second--;

            return true;
        }

        private bool ProgressMinute()
        {
            if (Minute <= 0)
            {
                if (!ProgressHour()) return false;
                Minute = 60 + Minute;
            }
            Minute--;

            return true;
        }

        private bool ProgressHour()
        {
            if (Hour <= 0)
            {
                return false;
            }

            Hour--;
            return true;
        }

        protected virtual void TimerEnd()
        {
            CountdownEnd.Value = true;
            Phase.Value = TimerPhase.IsIdle;
        }
    }
}
