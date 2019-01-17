using DomainModel;
using Framework.EF.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace DomainDatabaseMapping.Mappings
{
    public class AbstractBaseEntityMap : BaseAbstractMap
    {

        public AbstractBaseEntityMap(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public void Configure()
        {
           
            foreach (var entityType in this.ModelBuilder.Model.GetEntityTypes()
                        .Where(e => typeof(AbstractBaseEntity).IsAssignableFrom(e.ClrType)))
            {
                this.ModelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(AbstractBaseEntity.CreatedAt))

                    .HasAnnotation("ColumnOrder", 100)
                    .ValueGeneratedOnAdd()
                    .IsRequired(true)
                    .HasDefaultValueSql("getutcdate()")
                    ;

                this.ModelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(AbstractBaseEntity.CreatedBy))
                    .HasColumnType("nvarchar(100)")
                    .HasAnnotation("ColumnOrder", 101)
                    .ValueGeneratedOnAdd()
                    .IsRequired(true)
                    .HasDefaultValueSql("SYSTEM_USER")
                    ;

                this.ModelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(AbstractBaseEntity.UpdatedAt))
                    .HasAnnotation("ColumnOrder", 102)
                    .IsRequired(false)
                    ;

                this.ModelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(AbstractBaseEntity.UpdatedBy))
                    .HasColumnType("nvarchar(100)")
                    .HasAnnotation("ColumnOrder", 103)
                    .IsRequired(false)
                    ;
            }

        }
    }
}
