using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LaLaTimer.Models
{
    public class TimerTime : NotificationObject
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public TimerTime() { }

        public TimerTime(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }
    }
}
