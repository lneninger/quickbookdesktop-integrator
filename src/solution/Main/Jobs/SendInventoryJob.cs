using ApplicationLogic.Commands.QuickbooksIntegrator.SyncInventoryItems;
using ApplicationLogic.Interfaces.Repositories.Database;
using ApplicationLogic.Quickbooks;
using DatabaseSchema;
using Framework.Autofac;
using Framework.Logging.Log4Net;
//using QbSync.WebConnector.Core;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Main.Jobs
{
    [DisallowConcurrentExecution()]
    public class SendInventoryJob : IJob
    {
        protected LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Initializing Job Execution");
            IQuickbookTrackRepository quickbookTrackRepository = null;
            QuickbookExecution currentExecution = null;
            try
            {
                quickbookTrackRepository = IoCGlobal.Resolve<IQuickbookTrackRepository>();
                currentExecution = quickbookTrackRepository.AddExecution();

                //// Test Quickbook SessionManager
                //Logger.Info($"Initializing QB Manager");
                //using (var qbManager = IoCGlobal.Resolve<SessionManager>())
                //{

                    // Test Inventory Item Request
                    using (var syncInventoryItemsCommand = IoCGlobal.Resolve<ISyncInventoryItemsCommand>())
                    {
                        Logger.Info($"Creating Synchronization Manager");
                        var result = syncInventoryItemsCommand.Execute();
                        if (result.IsSucceed)
                        {
                            Logger.Info("Sync Inventory Items with success!");
                        }
                        else
                        {
                            Logger.Error("Finishing inventory items synchronization unsuccessful!");
                        }

                        //throw new NotImplementedException();
                        Logger.Info($"Saving Local Execution");
                        quickbookTrackRepository.SetExecutionStatus(currentExecution.Id, ExecutionStatusEnum.Success);

                        Console.WriteLine($"Finishing Job Execution");
                    }
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Logger.Info($"Execution error. {ex.Message}");
                if (currentExecution != null)
                {
                    quickbookTrackRepository.SetExecutionStatus(currentExecution.Id, ExecutionStatusEnum.Error);
                }
            }

            return Task.FromResult((object)null);
        }
    }
}
