using DomainModel;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class IntegrationProcessMap : BaseAbstractMap, IEntityTypeConfiguration<IntegrationProcess>
    {

        public IntegrationProcessMap(ModelBuilder modelBuilder): base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<IntegrationProcess> builder)
        {
            builder.ToTable("IntegrationProcess", SCHEMAS.INVENTORY);
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();


            // Seed
        }
    }
}
