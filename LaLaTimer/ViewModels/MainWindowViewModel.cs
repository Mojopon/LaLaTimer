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
using System.Windows.Shell;
using Reactive.Bindings;
using System.Reactive.Linq;
using Reactive.Bindings.Extensions;

namespace LaLaTimer.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        #region ProgressState変更通知プロパティ
        private TaskbarItemProgressState _ProgressState;

        public TaskbarItemProgressState ProgressState
        {
            get
            { return _ProgressState; }
            set
            { 
                if (_ProgressState == value)
                    return;
                _ProgressState = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Progress変更通知プロパティ
        private double _Progress;

        public double Progress
        {
            get
            { return _Progress; }
            set
            { 
                if (_Progress == value)
                    return;
                _Progress = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public object Content { get; set; }

        public MainWindowViewModel()
        {
            CompositeDisposable = new LivetCompositeDisposable();

            Content = new TimerContentViewModel();
            LaLaTimerClient.Current.Timer.Subscribe(OnChangeTimer);
        }

        void OnChangeTimer(ITimer timer)
        {
            if (CompositeDisposable.Count > 0) CompositeDisposable.Dispose();

            timer.Progress
                 .SkipLast(1)
                 .Subscribe(x => Progress = x)
                 .AddTo(CompositeDisposable);

            timer.Phase
                 .Subscribe(x =>
                 {
                     switch (x)
                     {
                         case TimerPhase.IsIdle:
                             ProgressState = TaskbarItemProgressState.None;
                             break;
                         case TimerPhase.IsRunning:
                             ProgressState = TaskbarItemProgressState.Normal;
                             break;
                         case TimerPhase.IsStopped:
                             ProgressState = TaskbarItemProgressState.Paused;
                             break;
                     }
                 }).AddTo(CompositeDisposable);
        }

        public void Initialize()
        {
        }
    }
}
