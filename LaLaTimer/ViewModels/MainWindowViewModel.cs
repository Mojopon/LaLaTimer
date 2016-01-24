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
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */


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
