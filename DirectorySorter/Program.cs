using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DirectorySorter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase.Run(new DirectoryScanner(Settings.DirectoryToScan, Settings.FileWhiteList, Settings.PollingInterval));
        }
    }
}
