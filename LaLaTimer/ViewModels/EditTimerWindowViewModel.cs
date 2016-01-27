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
    public class EditTimerWindowViewModel : ViewModel
    {

        #region Content変更通知プロパティ
        private IDisposable _Content;

        public IDisposable Content
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

        public EditTimerWindowViewModel(ITimer timer)
        {
            CompositeDisposable = new LivetCompositeDisposable();

            LaLaTimerEditor.Current.Edit(timer);

            LaLaTimerEditor.Current.Timer.Subscribe(x =>
            {
                if (Content != null) Content.Dispose();

                var type = x.GetType();

                if (type == typeof(CountdownTimer))
                {
                    Content = new EditCountdownTimerContentViewModel();
                }
                else if (type == typeof(PomodoroTimer))
                {
                    Content = new EditPomodoroTimerContentViewModel();
                }
            });
        }

        public void Initialize()
        {
        }
    }
}
