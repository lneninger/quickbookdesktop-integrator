using DomainModel;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class PriceLevelMap : BaseAbstractMap, IEntityTypeConfiguration<PriceLevel>
    {

        public PriceLevelMap(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<PriceLevel> builder)
        {
            builder.ToTable("PriceLevel", SCHEMAS.INVENTORY);
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasMaxLength(6);

            builder.Property(t => t.ExternalId)
                .HasMaxLength(32);

            builder.Property(t => t.Name)
               .HasColumnType("nvarchar(50)")
               .IsRequired();

            builder.Property(t => t.IsActive)
               .IsRequired();

            builder.Property(t => t.PriceLevelPercentage)
              .IsRequired(false);

            builder.Property(t => t.PriceLevelType)
                .HasMaxLength(15)
              .IsRequired();


            // Seed
        }
    }
}
