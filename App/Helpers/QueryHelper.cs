using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ProductValidation.Helpers
{
    public static class QueryHelper
    {
        public static async Task<PageResponse<T>> ApplyQuery<T>(
            IQueryable<T> query,
            QueryParams queryParams)
        {
            // SEARCH
            if (!string.IsNullOrEmpty(queryParams.Search))
            {
                query = query.Where("Name.Contains(@0)", queryParams.Search);
            }

            // FILTER
            if (!string.IsNullOrEmpty(queryParams.FilterBy) &&
                !string.IsNullOrEmpty(queryParams.FilterValue))
            {
                query = query.Where($"{queryParams.FilterBy} == @0", queryParams.FilterValue);
            }

            // SORT
            if (!string.IsNullOrEmpty(queryParams.SortBy))
            {
                var order = queryParams.Descending ? "descending" : "ascending";
                query = query.OrderBy($"{queryParams.SortBy} {order}");
            }

            // TOTAL COUNT
            var count = await query.CountAsync();

            // PAGINATION
            var items = await query
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new PageResponse<T>(
                items,
                count,
                queryParams.PageNumber,
                queryParams.PageSize
            );
        }
    }
}
   