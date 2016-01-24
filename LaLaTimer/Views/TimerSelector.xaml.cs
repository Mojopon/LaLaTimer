using LaLaTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaLaTimer.Views
{
    /// <summary>
    /// Interaction logic for TimerSelector.xaml
    /// </summary>
    public partial class TimerSelector : UserControl
    {
        public TimerSelector()
        {
            InitializeComponent();

            LaLaTimerClient.Current.Timer.Subscribe(OnChangeTimer);
        }

        void OnChangeTimer(ITimer timer)
        {
            timerSelectComboBox.SelectedIndex = LaLaTimerClient.Current.TimerIndex(timer);
        }
    }
}
