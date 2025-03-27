using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopApi.Shared.Models
{
    public class PagedList<T> where T : class
    {
        public List<T> Items { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        /// <summary>
        /// Creates a paginated list from the given source.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source.</typeparam>
        /// <param name="source">The source queryable collection to paginate.</param>
        /// <param name="pageNumber">The number of the page to retrieve (1-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A <see cref="PagedList{T}"/> containing the items for the specified page, 
        /// along with pagination metadata such as total item count and page size.</returns>
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}