using DomainDatabaseMapping.Mappings;
using DomainModel;
using Framework.EF.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
        public DbSet<IntegrationProcess> IntegrationProcesses { get; set; }
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
            modelBuilder.ApplyConfiguration(new IntegrationProcessMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new InventoryItemMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new AccountMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new AccountTypeMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new PriceLevelMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new PriceLevelInventoryItemMap(modelBuilder));
        }



        //
        // Summary:
        //     Saves all changes made in this context to the database.
        //
        // Returns:
        //     The number of state entries written to the database.
        //
        // Exceptions:
        //   T:Microsoft.EntityFrameworkCore.DbUpdateException:
        //     An error is encountered while saving to the database.
        //
        //   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
        //     A concurrency violation is encountered while saving to the database. A concurrency
        //     violation occurs when an unexpected number of rows are affected during save.
        //     This is usually because the data in the database has been modified since it was
        //     loaded into memory.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        public override int SaveChanges() {

            var entries = this.ChangeTracker.Entries().Where(t => typeof(AbstractBaseEntity).IsAssignableFrom(t.Entity.GetType()));
            var modifiedEntries = entries.Where(entry => entry.State == EntityState.Modified);
            foreach (var entry in modifiedEntries)
            {
                ((AbstractBaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }


            return base.SaveChanges();
        }

    }
}
