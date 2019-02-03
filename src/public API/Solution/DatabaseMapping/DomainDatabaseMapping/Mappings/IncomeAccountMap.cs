using DomainModel;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class IncomeAccountMap : BaseAbstractMap, IEntityTypeConfiguration<IncomeAccount>
    {

        public IncomeAccountMap(ModelBuilder modelBuilder): base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<IncomeAccount> builder)
        {
            builder.ToTable("IncomeAccount", SCHEMAS.INVENTORY);
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasMaxLength(100);

            builder.Property(t => t.FullName)
               .HasMaxLength(250)
               .IsRequired(true);

            // Seed
        }
    }
}
