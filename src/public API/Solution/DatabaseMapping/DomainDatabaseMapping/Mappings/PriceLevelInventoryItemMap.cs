using DomainModel;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class PriceLevelInventoryItemMap : BaseAbstractMap, IEntityTypeConfiguration<PriceLevelInventoryItem>
    {

        public PriceLevelInventoryItemMap(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<PriceLevelInventoryItem> builder)
        {
            builder.ToTable("PriceLevelInventoryItem", SCHEMAS.INVENTORY);
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasMaxLength(6);

            builder.Property(t => t.CustomPrice)
                .IsRequired(false);

            builder.Property(t => t.CustomPricePercent)
                .IsRequired(false);

            builder.Property(t => t.Type)
              .IsRequired(false);

            builder.HasOne(t => t.PriceLevel)
                .WithMany(p => p.InventoryItems)
                .HasForeignKey(t => t.PriceLevelId);


            builder.HasOne(t => t.InventoryItem)
                .WithMany()
                .HasForeignKey(t => t.InventoryItemId);
            // Seed
        }
    }
}
