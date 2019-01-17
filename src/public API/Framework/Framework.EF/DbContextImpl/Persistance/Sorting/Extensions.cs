using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Framework.Core.ReflectionHelpers;

namespace Framework.EF.DbContextImpl.Persistance.Models.Sorting
{
    public static class Extensions
    {
        public static IQueryable<T> ProcessSorting<T>(this IQueryable<T> query, SortingDTO<T> sorting)
        {
            var sortBys = GenerateSort<T>(sorting);

            foreach (var sortTuple in sortBys)
            {
                switch (sortTuple.SortOrder.ToUpper())
                {
                    case "DESC":
                        query = !query.IsOrdered<T>() ? Queryable.OrderByDescending(query, sortTuple.SortExpression) : Queryable.ThenBy((query as IOrderedQueryable<T>), sortTuple.SortExpression);
                        break;

                    case "ASC":
                    case "":
                    default:
                        query = !query.IsOrdered<T>() ? Queryable.OrderBy(query, sortTuple.SortExpression) : Queryable.ThenBy((query as IOrderedQueryable<T>), sortTuple.SortExpression);
                        break;

                }
            }

            return query;
        }

        public static List<SortItem<T>> GenerateSort<T>(SortingDTO<T> sorting)
        {
            List<SortItem<T>> sortItems = GetSortElements<T>(sorting);

            LambdaExpression untyped;
            var result = sortItems;
            bool outScopedOrderingPresent = sorting != null;
            for (var i = 0; i < result.Count; i++)
            {
                if (result[i].SortExpression != null) continue;

                var externalSort = sorting.SortExpressions.FirstOrDefault(o => o.PropertyName.Equals(sortItems[i].PropertyName, StringComparison.InvariantCultureIgnoreCase));

                if (externalSort != null)
                {
                    result[i].SortExpression = externalSort.SortExpression;
                }
                else
                {
                    try
                    {
                        untyped = GeneratePropertyExpression<T>(sortItems[i].PropertyName, sorting.CustomFilterPropertyMapping);
                        if (untyped != null)
                        {
                            result[i].SortExpression = untyped;
                        }
                    }
                    catch (Exception)
                    {
                        // mark to remove failed sortItem from list
                        result[i].Delete = true;
                    }
                }
            }

            result = result.Where(o => !o.Delete).ToList();

            return result;
        }

        public static List<SortItem<T>> GetSortElements<T>(SortingDTO<T> sorting)
        {
            List<string> propertySorts = new List<string>();
            if (sorting.RawSorting == null)
            {
                return sorting.DefaultSortItemsDelegate().ToList();
            }


            var result = new List<SortItem<T>>();
            foreach(var rawItem in sorting.RawSorting)
            {
                result.Add(new SortItem<T>
                {
                    PropertyName = rawItem.Key,
                    SortOrder = (rawItem.Value ?? string.Empty).Length > 1 ? rawItem.Value : "asc"
                });
            }
            
            
            return result;
        }

        private static LambdaExpression GeneratePropertyExpression<T>(string columnName, Dictionary<string, string> customFilterPropertyMapping)
        {
            string propertyName = columnName.ToPascalCase();

            if (customFilterPropertyMapping != null)
            {
                var matchMapping = customFilterPropertyMapping.ContainsKey(propertyName) ? customFilterPropertyMapping[propertyName] : null;
                if (matchMapping != null)
                {
                    propertyName = matchMapping;
                }
            }

            var parameter = Expression.Parameter(typeof(T));
            Expression memberExpression = parameter;
            foreach (var member in propertyName.Split('.'))
            {
                memberExpression = Expression.PropertyOrField(memberExpression, member);
            }

            var lambdaExpression = Expression.Lambda(memberExpression, parameter);
            LambdaExpression untyped = lambdaExpression;
            return untyped;
        }
    }
}
