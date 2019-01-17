using DomainDatabaseMapping;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using System;
using System.Collections.Generic;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRepositories
{
    public class AbstractDBRepository : IDBRepository
    {
        public AbstractDBRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            this.AmbientDbContextLocator = ambientDbContextLocator;
        }

        public IAmbientDbContextLocator AmbientDbContextLocator { get; }

        RiverdaleDBContext DbContext
        {
            get
            {
                var dbContext = this.AmbientDbContextLocator.Get<RiverdaleDBContext>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type UserManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                dbContext.Database.EnsureCreated();
                return dbContext;
            }
        }

        public void Detach(AbstractBaseEntity entity)
        {
            var dbContext = this.AmbientDbContextLocator.Get<RiverdaleDBContext>();
            foreach (var entry in dbContext.Entry(entity).Navigations)
            {
                if (entry.CurrentValue is IEnumerable<AbstractBaseEntity> children)
                {
                    foreach (var child in children)
                    {
                        dbContext.Entry(child).State = EntityState.Detached;
                    }
                }
                else if (entry.CurrentValue is AbstractBaseEntity child)
                {
                    dbContext.Entry(child).State = EntityState.Detached;
                }
            }
            dbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
