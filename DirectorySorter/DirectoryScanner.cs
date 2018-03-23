using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using DirectoryHelper;

namespace DirectorySorter
{
    public partial class DirectoryScanner : ServiceBase
    {
        private string DirectoryToScan;
        private string FileWhiteList;
        private int PollingInterval;

        public DirectoryScanner(string[] args)
        {
            this.OnStart(args);
            Console.WriteLine("Testing app");
            this.OnStop();
        }

        public DirectoryScanner(string directoryToScan, string fileWhiteList, int pollingInterval)
        {
            this.DirectoryToScan = directoryToScan;
            this.FileWhiteList = fileWhiteList;
            this.PollingInterval = pollingInterval;

            InitializeComponent();
            Thread.CurrentThread.Name = "Custom Directory Scanner";
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleException);
        }

        private void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;

            string thread = string.Format(CultureInfo.CurrentCulture, "An unhandled exception occurred on thread {0}", Thread.CurrentThread.Name);

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(exception.ToString(), EventLogEntryType.Error);
            }
        }

        protected override void OnStart(string[] args)
        {
            DebugMode();

            DirectoryFeeder.Start(@"C:\Users\smith83\Downloads", @".pdf/.exe/.crl/.doc/.docx/.xls/.xlsx/.zip", 300000);
        }

        protected override void OnStop()
        {
            DirectoryFeeder.Stop();
        }

        [Conditional("DEBUG_SERVICE")]
        private static void DebugMode()
        {
            Debugger.Break();
        }
    }
}
