using DomainModel;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class AccountTypeMap : BaseAbstractMap, IEntityTypeConfiguration<AccountType>
    {

        public AccountTypeMap(ModelBuilder modelBuilder): base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.ToTable("AccountType", SCHEMAS.INVENTORY);
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasMaxLength(10);

            builder.Property(t => t.Name)
               .HasMaxLength(50)
               .IsRequired(true);

            // Seed
            builder.HasData(
                new AccountType { Id = "INCOME", Name = "Income Account" }
                , new AccountType { Id = "INVENTORY", Name = "Inventory Account" }
            );
        }
    }
}
