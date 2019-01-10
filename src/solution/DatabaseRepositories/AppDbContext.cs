using DatabaseSchema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public DbSet<QuickbookState> States { get; set; }

        public DbSet<QuickbookTicket> Tickets { get; set; }

    }
}
