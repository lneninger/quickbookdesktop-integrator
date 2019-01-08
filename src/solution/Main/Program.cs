using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MainService()
            };
#if(DEBUG)
            ((MainService)ServicesToRun[0]).DebugStart();
#else

            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
