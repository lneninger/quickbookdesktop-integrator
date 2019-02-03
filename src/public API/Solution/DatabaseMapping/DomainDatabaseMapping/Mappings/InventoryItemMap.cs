using DomainModel;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class InventoryItemMap : BaseAbstractMap, IEntityTypeConfiguration<InventoryItem>
    {

        public InventoryItemMap(ModelBuilder modelBuilder): base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.ToTable("InventoryItem", SCHEMAS.INVENTORY);
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasMaxLength(6);

            builder.Property(t => t.Name)
               .HasColumnType("nvarchar(50)")
               .IsRequired(true);

            builder.Property(t => t.FullName)
               .HasColumnType("nvarchar(50)")
               .IsRequired(true);

            builder.HasOne(t => t.IncomeAccount)
              .WithMany()
              .HasForeignKey(t => t.IncomeAccountId)
              .IsRequired(false);

            // Seed
        }
    }
}
