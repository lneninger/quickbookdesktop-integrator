using Framework.Autofac;
using Main.Jobs;
using Quartz;
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

namespace Main
{
    public partial class MainService : ServiceBase
    {
        public MainService()
        {
            InitializeComponent();
        }

        public void DebugStart()
        {
            this.OnStart(null);
            Thread.Sleep(90000);
        }

        protected override void OnStart(string[] args)
        {
            IoCConfig.Init();

            this.ConfigureJobs().ConfigureAwait(true);
            
        }

        private async Task ConfigureJobs()
        {
            var job = JobBuilder.Create<SendInventoryJob>().WithIdentity("Heartbeat", "Maintenance").Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("Heartbeat", "Maintenance")
                .StartNow()
                .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForTotalCount(1)).Build();
                //.WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(2)).Build();
            var cts = new CancellationTokenSource();

            var scheduler = IoCGlobal.Resolve<IScheduler>();

            await scheduler.ScheduleJob(job, trigger, cts.Token).ConfigureAwait(true);
            await scheduler.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
