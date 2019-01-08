using Framework.Autofac;
using QbSync.WebConnector.Core;
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

        public IAuthenticator Authenticator { get; }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var authenticator = IoCGlobal.Resolve<IAuthenticator>();

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
