using Microsoft.EntityFrameworkCore;

namespace EshopApi.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for working with Entity Framework Core.
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Converts the given <see cref="IQueryable{TSource}"/> to a <see cref="List{TSource}"/> asynchronously,
        /// ensuring compatibility with both synchronous and asynchronous query providers.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source query.</typeparam>
        /// <param name="source">The source query to convert to a list.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the list of elements
        /// from the source query.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="source"/> is null.</exception>
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