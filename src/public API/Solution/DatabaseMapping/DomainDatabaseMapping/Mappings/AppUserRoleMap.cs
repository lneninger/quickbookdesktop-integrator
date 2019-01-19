using DomainModel;
using DomainModel.Identity;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class AppUserRoleMap : BaseAbstractMap, IEntityTypeConfiguration<AppUserRole>
    {

        public AppUserRoleMap(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {

            builder.Property(t => t.Description)
              .HasColumnType("varchar(200)")
              .IsRequired(false);

        }
    }
}
