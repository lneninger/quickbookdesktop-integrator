using ApplicationLogic.Quickbooks;
using Framework.Autofac;
using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Main.ApplicationEvents;

namespace Main
{
    static class Program
    {
        #region Trap application termination
        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(ApplicationEvents.EventHandler handler, bool add);

        static bool exitSystem = false;
        static ApplicationEvents.EventHandler _applicationEventHandler;
        private static AutoResetEvent StopEvent = new AutoResetEvent(false);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(params string[] args)
        {
            // Some biolerplate to react to close window event, CTRL-C, kill, etc
            _applicationEventHandler += new ApplicationEvents.EventHandler(ApplicationEventHandler);
            SetConsoleCtrlHandler(_applicationEventHandler, true);

            if (Environment.UserInteractive)
            {
                if (args.Any(arg => arg.ToUpper() == "RUN"))
                {
                    new MainService().InteractiveStart();
                    // Console.ReadKey();
                }
                else
                {
                    ShowMenu();
                }
            }
            else
            {
                RunServices();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("1. Execute Once");
            Console.WriteLine("2. Execute Using Quartz Configuration");

            if (!ServiceIsInstalled("HIPALANET-QUICKBOOKS-INTEGRATOR"))
            {
                Console.WriteLine("3. Install Service");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("3. Uninstall Service");
                Console.ResetColor();
            }

            Console.WriteLine("4. Exit");

            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Executing application once...");
                    Console.ResetColor();
                    new MainService().InteractiveStart();
                    break;

                case "2":
                    new MainService().InteractiveStart(useQuartz: true);
                    break;

                case "3":
                    if (!ServiceIsInstalled("HIPALANET-QUICKBOOKS-INTEGRATOR"))
                    {
                        InstallService();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Uninstalling Service HIPALANET-QUICKBOOKS-INTEGRATOR");
                        UninstallService();
                        Console.WriteLine($"Uninstalled Service HIPALANET-QUICKBOOKS-INTEGRATOR");
                        Console.ResetColor();
                    }
                    break;

                case "4":
                    break;
            }

            Console.CancelKeyPress += new ConsoleCancelEventHandler(myHandler);


            while (true)
            {
                Thread.Sleep(500);
            }

        }

        private static void myHandler(object sender, ConsoleCancelEventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Application Stopping.");
            Console.ResetColor();
            Thread.Sleep(1500);
            args.Cancel = true;
        }

        private static void WaitStop() {
            Console.ReadLine();
            Console.WriteLine("Application will stop");
        }


        private static bool ServiceIsInstalled(string serviceName)
        {
            var serviceController = ServiceController.GetServices()
    .FirstOrDefault(s => s.ServiceName == serviceName);
            return (serviceController != null);
        }

        private static void InstallService()
        {
            ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
        }

        private static void UninstallService()
        {
            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
        }

        private static void RunServices()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[]
                        {
                new MainService()
                        };
//#if (DEBUG)
//            ((MainService)ServicesToRun[0]).InteractiveStart();
//#else

            ServiceBase.Run(ServicesToRun);
//#endif
        }


        private static bool ApplicationEventHandler(CtrlType sig)
        {
            Console.WriteLine("Exiting system due to external CTRL-C, or process kill, or shutdown");

            //do your cleanup here
            switch (sig)
            {
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                    using (var qbManager = IoCGlobal.Resolve<SessionManager>())
                    {
                        qbManager.closeConnection();
                    }
                    break;
            }
            Thread.Sleep(5000); //simulate some cleanup delay

            Console.WriteLine("Cleanup complete");

            //allow main to run off
            exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }

    }




    public class ApplicationEvents {


        
        public delegate bool EventHandler(CtrlType sig);

        public enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        
        #endregion

    }
}

