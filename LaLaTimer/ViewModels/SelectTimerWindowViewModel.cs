using Livet;
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

        public SelectTimerWindowViewModel()
        {
            CompositeDisposable = new LivetCompositeDisposable();
            TimerSelector = new TimerSelectorViewModel().AddTo(CompositeDisposable);
        }

        public void Initialize() { }
    }
}
