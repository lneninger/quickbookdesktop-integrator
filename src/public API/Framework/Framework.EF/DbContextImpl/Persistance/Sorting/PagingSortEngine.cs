using Framework.EF.DbContextImpl.Persistance.Models.Sorting;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using LMB.PredicateBuilderExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Framework.EF.DbContextImpl.Persistance
{
    public static class PagingSortEngine
    {
        public static PageResult<T> ProcessPagingSort<T>(this IQueryable<T> query, Expression<Func<T, bool>> filterPredicate, IPaging paging, SortingDTO<T> sorting) where T : class
        {
            return query.ProcessPagingSort<T, T>(filterPredicate, paging, sorting, null);
        }

        public static PageResult<R> ProcessPagingSort<T, R>(this IQueryable<T> query, Expression<Func<T, bool>> filterPredicate, IPaging paging, SortingDTO<T> sorting, Expression<Func<T, R>> mapping) where R : class
        {
            // Get Total Count
            var totalCount = query.Count();

            // Filter
            query = query.Where(filterPredicate);

            // Get Filtered Count
            var filteredCount = query.Count();

            // Apply Order
            query = query.ProcessSorting<T>(sorting);

            //Paging
            query = (paging != null) ? query.Skip(paging.PageSize * paging.PageIndex).Take(paging.PageSize) : query;

            // Mapping
            var castedQuery = (mapping != null) ? query.Select(mapping) : query.Cast<R>();

            // Get Items
            var queryResult = castedQuery.ToList();

            var result = new PageResult<R>
            {
                FilteredCount = filteredCount,
                TotalCount = totalCount,
                Size = paging == null ? 0 : paging.PageSize,
                Items = queryResult
            };

            return result;
        }
    }
}
