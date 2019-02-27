using DomainDatabaseMapping.Mappings;
using DomainModel;
using Framework.EF.Logging;
using Microsoft.EntityFrameworkCore;

namespace DomainDatabaseMapping
{
    public class ApplicationDBContext : IdentityDBContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDBContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseLoggerFactory(CustomLoggerFactory.LoggerFactoryImpl);
        }

        /********************************SECURITY*********************************/
        // Security


        /*********************************CRM  Master Tables**********************/
        // Inventory
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<IncomeAccount> IncomeAccounts { get; set; }
        public DbSet<InventoryAccount> InventoryAccounts { get; set; }
        public DbSet<PriceLevel> PriceLevels { get; set; }
        public DbSet<PriceLevelInventoryItem> PriceLevelInventoryItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Base Entity Class
            new AbstractBaseEntityMap(modelBuilder).Configure();

            // Inventory
            modelBuilder.ApplyConfiguration(new InventoryItemMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new AccountMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new AccountTypeMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new PriceLevelMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new PriceLevelInventoryItemMap(modelBuilder));
        }


    }
}
