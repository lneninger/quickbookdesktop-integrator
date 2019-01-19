using DomainModel;
using DomainModel.Identity;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DomainDatabaseMapping.Mappings
{
    public class AppUserMap : BaseAbstractMap, IEntityTypeConfiguration<AppUser>
    {

        public AppUserMap(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            //builder.Property(t => t.PasswordHash)
            //  .HasColumnType("varbinary(MAX)")
            //  .IsRequired(false);

            //builder.Property(t => t.PasswordSalt)
            //  .HasColumnType("varbinary(MAX)")
            //  .IsRequired(false);

        }
    }
}
