using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Framework.EF.DbContextImpl
{
    public static class DbContextExtensions
    {
        public static IDictionary<string, object> GetKeys<T>(this DbContext ctx) where T: class
        {
            var entity = (T)Activator.CreateInstance<T>();
            if (ctx == null)
            {
                throw new ArgumentNullException(nameof(ctx));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = ctx.Entry(entity);
            var primaryKey = entry.Metadata.FindPrimaryKey();
            var keys = primaryKey.Properties.ToDictionary(x => x.Name, x => x.PropertyInfo.GetValue(entity));

            return keys;
        }

        public static IEnumerable<string> GetDirtyProperties(this DbContext ctx, object entity)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException(nameof(ctx));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = ctx.Entry(entity);
            var originalValues = entry.OriginalValues;
            var currentValues = entry.CurrentValues;

            foreach (var prop in originalValues.Properties)
            {
                if (object.Equals(originalValues[prop.Name], currentValues[prop.Name]) == false)
                {
                    yield return prop.Name;
                }
            }
        }
    }
}
