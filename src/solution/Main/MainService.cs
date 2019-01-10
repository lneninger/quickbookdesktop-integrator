using Framework.Autofac;
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
            this.CreateDatabaseFile();
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


        private void CreateDatabaseFile()
        {
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
                SQLiteConnection conn = null;
                try
                {
                    conn = connectionFactory.CreateConnection() as SQLiteConnection;

                    CreateTicketTable(conn);
                    CreateStateTable(conn);
                }
                finally
                {
                    if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void CreateTicketTable(SQLiteConnection conn)
        {
            string sql = @"CREATE TABLE [QuickbookState] (
                                  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                , [Ticket] nvarchar(100) NOT NULL COLLATE NOCASE
                                , [Authenticated] bit NOT NULL
                              );";

            this.CreateTable(sql, conn);
        }

        private void CreateStateTable(SQLiteConnection conn)
        {
            string sql = @"CREATE TABLE [QuickbookTicket] (
                                  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                , [Ticket] nvarchar(100) NOT NULL COLLATE NOCASE
                                , [CurrentStep] nvarchar(100) NULL COLLATE NOCASE
                                , [Key] nvarchar(100) NULL COLLATE NOCASE
                              );";

            this.CreateTable(sql, conn);
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
