using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using Livet.Messaging;
using LaLaTimer.Models;
using Livet.Commands;

namespace LaLaTimer.ViewModels
{
    public class TimerContentViewModel : ViewModel
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
            LaLaTimerClient.Current.Timer.Subscribe(OnChangeTimer);
        }

        void OnChangeTimer(ITimer timer)
        {
            if (timer == null) return;

            if (typeof(PomodoroTimer) == timer.GetType())
            {
                Content = new PomodoroTimerViewModel();
            }
            else if(typeof(CountdownTimer) == timer.GetType())
            {
                Content = new CountdownTimerViewModel();
            }
        }

        #region EditCommand
        private ViewModelCommand _EditCommand;

        public ViewModelCommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new ViewModelCommand(Edit, CanEdit);
                }
                return _EditCommand;
            }
        }

        public bool CanEdit()
        {
            return true;
        }

        public void Edit()
        {
            Messenger.Raise(new TransitionMessage(new SelectTimerWindowViewModel(), "EditCommand"));
        }
        #endregion
    }
}
