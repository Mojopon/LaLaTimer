using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using LaLaTimer.Models;

namespace LaLaTimer.ViewModels
{
    public class TimerContentViewModel : NotificationObject
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

        public TimerContentViewModel()
        {
            LaLaTimerClient.Current.OnChangeTimer.Subscribe(OnChangeTimer);
        }

        void OnChangeTimer(ITimer timer)
        {
            if (typeof(PomodoroTimer) == timer.GetType())
            {
                Console.WriteLine("Pomodoro timer");
                Content = new PomodoroTimerViewModel();
            }
            else if(typeof(CountdownTimer) == timer.GetType())
            {
                Console.WriteLine("Countdown Timer");
                Content = new CountdownTimerViewModel();
            }
        }
    }
}
