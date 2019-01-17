using ApplicationLogic.AppConfiguration;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems;
using ApplicationLogic.Quickbooks;
using Framework.Autofac;
using Framework.Logging.Log4Net;
//using QbSync.WebConnector.Core;
using Quartz;
using QuickbookRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Jobs
{
    public class SendInventoryJob : IJob
    {
        protected LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                // Test Quickbook SessionManager
                var qbManager = IoCGlobal.Resolve<SessionManager>();

                // Test Inventory Item Request
                var getInventoryItemsCommand = IoCGlobal.Resolve<IGetInventoryItemsCommand>();
                var result = getInventoryItemsCommand.Execute();
                Logger.Info("Sync Inventory Items with success!");

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return Task.FromResult((object)null);
        }
    }
}
