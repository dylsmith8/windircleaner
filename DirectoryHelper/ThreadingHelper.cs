using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryHelper
{
    public class ThreadingHelper
    {
        public static ManualResetEvent _stopEvent;
        private  Thread _thread;
        public static bool ServiceIsRunning { get; set; }

        public ThreadingHelper(Thread thread, ManualResetEvent manualResetEvent)
        {
            _thread = thread;
            _stopEvent = manualResetEvent;
        } 
        
        public void StartThread()
        {
            _thread.Start();
            ServiceIsRunning = true;
            _stopEvent.Reset();
        }
    }
}
