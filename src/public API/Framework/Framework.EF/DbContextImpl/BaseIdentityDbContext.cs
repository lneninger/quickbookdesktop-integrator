using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Framework.EF.DbContextImpl.Persistance;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Framework.Autofac;

namespace Framework.EF.DbContextImpl
{
    public class BaseIdentityDbContext<T> : IdentityDbContext<T> where T : IdentityUser
    {
        public BaseIdentityDbContext() : base()
        {
        }

        public BaseIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            var changed = this.ChangeTracker.Entries<ITrackChangesEntity>();

            string userId = null;

            //using (var currentUserService = IoCGlobal.Resolve<ICurrentUserService>())
            //{
            //    userId = currentUserService.CurrentUserId;
            //}

            foreach (var changedEntity in changed.Where(o => o.State == EntityState.Added))
            {
                ((ITrackChangesEntity)changedEntity.Entity).CreatedAt = DateTime.UtcNow;
                ((ITrackChangesEntity)changedEntity.Entity).CreatedBy = userId;
            }

            foreach (var changedEntity in changed.Where(o => o.State == EntityState.Modified))
            {
                ((ITrackChangesEntity)changedEntity.Entity).UpdatedAt = DateTime.UtcNow;
                ((ITrackChangesEntity)changedEntity.Entity).UpdatedBy = userId;
            }

            return base.SaveChanges();
        }



        //[DbFunction]
        //public static string suser_sname()
        //{
        //    throw new Exception();
        //}

        //[DbFunction]
        //public static DateTime getutcdate()
        //{
        //    throw new Exception();
        //}
    }
}
