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

namespace LaLaTimer.Views.Controls
{
    /// <summary>
    /// Interaction logic for DigitalTimerInputField.xaml
    /// </summary>
    public partial class DigitalTimerInputField : UserControl
    {
        public DigitalTimerInputField()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HourProperty = DependencyProperty.Register("Hour", typeof(int), typeof(DigitalTimerInputField), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        public static readonly DependencyProperty MinuteProperty = DependencyProperty.Register("Minute", typeof(int), typeof(DigitalTimerInputField), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }

        public static readonly DependencyProperty SecondProperty = DependencyProperty.Register("Second", typeof(int), typeof(DigitalTimerInputField), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public int Second
        {
            get { return (int)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }
    }
}
