﻿using DomainDatabaseMapping.Mappings;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Base Entity Class
            new AbstractBaseEntityMap(modelBuilder).Configure();

            // Inventory
            modelBuilder.ApplyConfiguration(new InventoryItemMap(modelBuilder));
        }


    }
}