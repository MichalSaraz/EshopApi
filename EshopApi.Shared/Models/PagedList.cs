namespace EshopApi.Shared.Models
{
    /// <summary>
    /// Represents a paginated list of items.
    /// This class provides metadata about the pagination, such as the current page number,
    /// total number of pages, page size, and total item count.
    /// It also provides methods to create a paginated list from a source queryable collection.
    /// The items in the paginated list are of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> where T : class
    {
        /// <summary>
        /// Represents a paginated list of items.
        /// </summary>
        public List<T> Items { get; private set; }

        /// <summary>
        /// The current page number (1-based).
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// The total number of pages available.
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// The size of each page (number of items per page).
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// The total number of items available across all pages.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Indicates whether there are more pages available after the current page.
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// Indicates whether there are previous pages available before the current page.
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
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