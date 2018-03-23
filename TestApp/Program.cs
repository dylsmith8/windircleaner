using DirectorySorter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                DirectoryScanner ds = new DirectoryScanner(args);
                Console.Write("Service is running");
                Console.ReadLine();
            }
        }
    }
}
