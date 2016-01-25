using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLaTimer.ViewModels
{
    public class SelectTimerWindowViewModel : ViewModel
    {
        public object TimerSelector { get; } = new TimerSelectorViewModel();

        public SelectTimerWindowViewModel()
        {

        }

        public void Initialize() { }
    }
}
