using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLaTimer
{
    public class WindowService
    {
        public static WindowService Current { get; } = new WindowService();

        public WindowService()
        {

        }
    }
}
