using DomainDatabaseMapping.Mappings;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.IO;
using DomainModel.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DomainModel.File;
using DomainDatabaseMapping.Mappings.File;
using DomainDatabaseMapping.Mappings.Type;
using DomainDatabaseMapping.Mappings.Product;
using Framework.EF.Logging;
using DomainModel.SaleOpportunity;
using DomainDatabaseMapping.Mappings.SaleOpportunity;
using DomainModel.Product;

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
            modelBuilder.ApplyConfiguration(new InventoryMap(modelBuilder));
        }


    }
}
