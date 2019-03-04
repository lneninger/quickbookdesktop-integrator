using ApplicationLogic.AppConfiguration;
using Framework.Autofac;
using Framework.Logging.Log4Net;
using Main.Helpers;
using Main.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Main
{
    public partial class MainService : ServiceBase
    {
        protected LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IScheduler Scheduler { get; private set; }

        public MainService()
        {
            InitializeComponent();
        }

        public void InteractiveStart(bool useQuartz = false)
        {
            IoCConfig.Init();
            this.CreateDatabaseFile();

            MainService.Scheduler = AsyncHelpers.RunSync<IScheduler>(this.ConfigureJobs);
            var job = IoCGlobal.Resolve<SendInventoryJob>();
            MainService.Scheduler.Start();
            

        }

        protected override void OnStart(string[] args)
        {
            var userIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var userPrincipal = new System.Security.Principal.WindowsPrincipal(userIdentity);
            //var userPrincipal = Thread.CurrentPrincipal as System.Security.Principal.WindowsPrincipal;// System.Security.Principal.WindowsIdentity.GetCurrent();
            if (userPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                Logger.Fatal("User is not running the app as Administrator");
                //throw new SystemException("User is not running the app as Administrator");
            }

            IoCConfig.Init();
            this.CreateDatabaseFile();
            MainService.Scheduler = AsyncHelpers.RunSync<IScheduler>(this.ConfigureQuartz);
            MainService.Scheduler.Start();

        }

        private async Task<IScheduler> ConfigureQuartz()
        {
            var scheduler = IoCGlobal.Resolve<IScheduler>();
            scheduler = await ConfigureJobs(scheduler);
            return scheduler;
        }

        private async Task<IScheduler> ConfigureJobs()
        {
            return await this.ConfigureJobs(null);
        }

        private async Task<IScheduler> ConfigureJobs(IScheduler scheduler)
        {
            try
            {
                var appConfig = IoCGlobal.Resolve<AppConfig>();

                var job = JobBuilder.Create<SendInventoryJob>().WithIdentity("SendInventory", "QUICKBOOKS").Build();
                var trigger = TriggerBuilder.Create()
                    .WithIdentity("SendInventory", "QUICKBOOKS")
                     .WithCronSchedule(appConfig.QuartzScheduler)
                    .StartNow()

                //.WithSchedule(CronExpression. SimpleScheduleBuilder.RepeatSecondlyForTotalCount(1)).Build();
                //.WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(2))
                .Build();

                var cts = new CancellationTokenSource();

                if (scheduler == null)
                {
                    scheduler = IoCGlobal.Resolve<IScheduler>();
                }

                await scheduler.ScheduleJob(job, trigger, cts.Token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Fatal("Fatal error initializing integration execution", ex);
            }

            return scheduler;
        }

        protected override void OnStop()
        {
        }


        private void CreateDatabaseFile()
        {
            Logger.Info($"Creating Database File SQLite");

            var connectionFactory = new SqLiteConnectionFactory();
            var filePath = connectionFactory.GetFilePath();
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                SQLiteConnection.CreateFile(filePath);
            }

            SQLiteConnection conn = null;
            try
            {
                conn = connectionFactory.CreateConnection() as SQLiteConnection;

                DropTicketTable(conn);
                DropTicketTable(conn);
                CreateExecutionTable(conn);
                CreateExecutionStatusTable(conn);
            }
            catch (Exception ex)
            {
                Logger.Fatal("Problem configuring internal database.", ex);
            }
            finally
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void DropStateTable(SQLiteConnection conn)
        {
            string sql = @"DROP TABLE IF EXISTS [QuickbookState];";

            this.CreateTable(sql, conn);
        }

        private void DropTicketTable(SQLiteConnection conn)
        {
            string sql = @"DROP TABLE IF EXISTS [QuickbookTicket];";

            this.CreateTable(sql, conn);
        }

        private void CreateExecutionTable(SQLiteConnection conn)
        {
            string sql = @"CREATE TABLE IF NOT EXISTS [QuickbookExecution] (
                                  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                , [Date] DATETIME NOT NULL
                                , [StatusId]  VARCHAR(6) NOT NULL
                              );";

            this.CreateTable(sql, conn);
        }

        private void CreateExecutionStatusTable(SQLiteConnection conn)
        {
            string sql = @"CREATE TABLE IF NOT EXISTS [ExecutionStatus] (
                                  [Id] VARCHAR(6) PRIMARY KEY NOT NULL
                                , [Name] VARCHAR(100) NOT NULL COLLATE NOCASE UNIQUE
                              );";

            this.CreateTable(sql, conn);

            // Seed
            new SQLiteCommand("INSERT OR IGNORE INTO ExecutionStatus([Id], [Name]) values('EXEC', 'Executing')-- WHERE NOT EXISTS(SELECT 1 FROM ExecutionStatus WHERE Id = 'EXEC')", conn).ExecuteNonQuery();
            new SQLiteCommand("INSERT OR IGNORE INTO ExecutionStatus([Id], [Name]) values('SUCC', 'Success')-- WHERE NOT EXISTS(SELECT 1 FROM ExecutionStatus WHERE Id = 'SUCC')", conn).ExecuteNonQuery();
            new SQLiteCommand("INSERT OR IGNORE INTO ExecutionStatus([Id], [Name]) values('ERROR', 'Error')-- WHERE NOT EXISTS(SELECT 1 FROM ExecutionStatus WHERE Id = 'ERROR')", conn).ExecuteNonQuery();
        }

        private void CreateTable(string tableCreateCommand, SQLiteConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var command = new SQLiteCommand(tableCreateCommand, connection);
            command.ExecuteNonQuery();
        }
    }
}
