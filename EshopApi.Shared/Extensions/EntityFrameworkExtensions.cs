using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EshopApi.Shared.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static Task<List<TSource>> ToListAsyncSafe<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source is IAsyncEnumerable<TSource>)
                return source.ToListAsync();

            return Task.FromResult(source.ToList());
        }
    }
}