using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DirectoryHelper
{
    public class DirectoryFeeder
    {
        private string _directoryToScan { get; set; }
        private string _fileWhiteList { get; set; }
        private int _pollingInterval { get; set; }

        public DirectoryFeeder(string directoryToScan, string fileWhiteList, int pollingInterval)
        {
            _directoryToScan = directoryToScan;
            _fileWhiteList = fileWhiteList;

            Thread thread = new Thread(new ThreadStart(ScanDirectory))
            {
                Name = "Directory scanner thread instance"
            };

            ThreadingHelper threadingHelper = new ThreadingHelper(thread, new ManualResetEvent(false));
            threadingHelper.StartThread();
        }

        public void ScanDirectory()
        {
            while (ThreadingHelper.ServiceIsRunning)
            {
                if (!Directory.Exists(_directoryToScan))
                {
                    throw new InvalidOperationException("Specified directory does not exist");
                }

                try
                {
                    MoveFilesEngine.FindAndMoveFilesAsync(_directoryToScan, _fileWhiteList);
                }
                catch (Exception e)
                {
                    using (EventLog eventLog = new EventLog("Application"))
                    {
                        eventLog.Source = "Application";
                        eventLog.WriteEntry(e.ToString(), EventLogEntryType.Error);
                    }
                }
                finally
                {
                    if (ThreadingHelper.ServiceIsRunning)
                    {
                        ThreadingHelper._stopEvent.WaitOne(_pollingInterval, false);
                    }
                }
            }
        }

        public static void Start(string directoryToScan, string fileWhiteList, int pollingInterval)
        {
            new DirectoryFeeder(directoryToScan, fileWhiteList, pollingInterval);
        }

        public static void Stop()
        {
            ThreadingHelper.ServiceIsRunning = false;
            ThreadingHelper._stopEvent.Set();
        }
    }
}
