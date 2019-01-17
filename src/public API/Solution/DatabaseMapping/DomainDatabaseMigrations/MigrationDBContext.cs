using DomainDatabaseMapping.Mappings;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace DomainDatabaseMapping
{
    public class MigrationDBContext: ApplicationDBContext
    {
        public MigrationDBContext(DbContextOptions options) : base(options)
        {
        }

        public MigrationDBContext(): base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //string connStr = ConfigurationManager.ConnectionStrings["DomainModel"].ConnectionString;

            ////base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(connStr);
            optionsBuilder.UseSqlServer("Data Source=(local)\\SQLEXPRESS;Initial Catalog=riverdale;Integrated Security=SSPI;Persist Security Info=False;MultipleActiveResultSets=True;Application Name=Riverdale2.0");
        }
    }
}
