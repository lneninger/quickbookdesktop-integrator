using ApplicationLogic.AppConfiguration;
using ApplicationLogic.Quickbooks;
using Framework.Autofac;
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
        //public SendInventoryJob(IAuthenticator authenticator)
        //{
        //    this.Authenticator = authenticator;
        //}

        //public IAuthenticator Authenticator { get; }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                // Test Quickbook SessionManager
                var qbManager = IoCGlobal.Resolve<SessionManager>();

                // Test Inventory Item Request
                var inventoryRepository = IoCGlobal.Resolve<ApplicationLogic.Interfaces.Repositories.Quickbooks.IInventoryRepository>();
                inventoryRepository.Request();

                //var authenticator = IoCGlobal.Resolve<IAuthenticator>();

                //var appConfig = IoCGlobal.Resolve<AppConfig>();
                //qbManager.AuthenticateAsync(appConfig.UserName, appConfig.Password);

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
