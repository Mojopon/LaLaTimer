using LaLaTimer.Editor;
using LaLaTimer.Models;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.Windows;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLaTimer.ViewModels
{
    public class SelectTimerWindowViewModel : ViewModel
    {
        public TimerSelectorViewModel TimerSelector { get; private set; }

        private ITimer SelectedTimer;

        #region TimerIsSelected変更通知プロパティ
        private bool _TimerIsSelected;

        public bool TimerIsSelected
        {
            get
            { return _TimerIsSelected; }
            set
            { 
                if (_TimerIsSelected == value)
                    return;
                _TimerIsSelected = value;
                RaisePropertyChanged();

                EditCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        public SelectTimerWindowViewModel()
        {
            CompositeDisposable = new LivetCompositeDisposable();
            TimerSelector = new TimerSelectorViewModel().AddTo(CompositeDisposable);
            TimerSelector.SelectedTimer
                         .Subscribe(timer => 
                         {
                             SelectedTimer = timer;
                             if(SelectedTimer == null)
                             {
                                 TimerIsSelected = false;
                             }
                             else
                             {
                                 TimerIsSelected = true;
                             }
                         })
                         .AddTo(CompositeDisposable);
        }

        public void Initialize() { }


        #region CreateCommand
        private ViewModelCommand _CreateCommand;

        public ViewModelCommand CreateCommand
        {
            get
            {
                if (_CreateCommand == null)
                {
                    _CreateCommand = new ViewModelCommand(Create, CanCreate);
                }
                return _CreateCommand;
            }
        }

        public bool CanCreate()
        {
            return true;
        }

        public void Create()
        {
            Messenger.Raise(new TransitionMessage(new EditTimerWindowViewModel(LaLaTimerEditor.Current.CreateNew(TimerType.PomodoroTimer)), "OpenEditWindow"));
        }
        #endregion


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
            return TimerIsSelected;
        }

        public void Edit()
        {
            Messenger.Raise(new TransitionMessage(new EditTimerWindowViewModel(SelectedTimer), "OpenEditWindow"));
        }
        #endregion

        #region OkCommand
        private ViewModelCommand _OkCommand;

        public ViewModelCommand OkCommand
        {
            get
            {
                if (_OkCommand == null)
                {
                    _OkCommand = new ViewModelCommand(Ok, CanOk);
                }
                return _OkCommand;
            }
        }

        public bool CanOk()
        {
            return true;
        }

        public void Ok()
        {
            LaLaTimerClient.Current.Select(SelectedTimer, true);
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }
        #endregion

    }
}
