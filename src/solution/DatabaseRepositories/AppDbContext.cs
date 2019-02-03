using DatabaseSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(IConnectionFactory connFactory) : base(connFactory.CreateConnection(), true)
        {
        }

        public DbSet<QuickbookExecution> QuickbookExecutions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<QuickbookExecution>().HasKey(t => t.Id);
            modelBuilder.Entity<QuickbookExecution>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
