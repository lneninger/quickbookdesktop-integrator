using DomainModel;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class AccountMap : BaseAbstractMap, IEntityTypeConfiguration<Account>
    {

        public AccountMap(ModelBuilder modelBuilder): base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", SCHEMAS.INVENTORY);
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ExternalId)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(t => t.FullName)
               .HasMaxLength(250)
               .IsRequired(true);


            builder.Property(t => t.AccountTypeId)
                .HasMaxLength(10)
               .IsRequired(true);

            builder.HasOne(t => t.AccountType)
                .WithMany()
                .HasForeignKey(t => t.AccountTypeId)
                .IsRequired(false);

            builder.HasDiscriminator(t => t.AccountTypeId)
            .HasValue<IncomeAccount>("INCOME")
            .HasValue<InventoryAccount>("INVENTORY");

            // Seed
        }
    }
}
