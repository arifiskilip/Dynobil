using System.Security.Principal;

namespace WebUI.Paging
{
    public class Paginate<T> : IPaginate<T>
        where T : class, new()
    {
        public IQueryable<T> Items { get; }
        public PaginationInfo Pagination { get; }


        private Paginate(IQueryable<T> items, int pageIndex, int pageSize, IQueryable<T> source)
        {
            Items = items;
            Pagination = new PaginationInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = source.Count(),
                TotalPages = (int)Math.Ceiling(source.Count() / (double)pageSize)
            };
        }
        public static Paginate<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new Paginate<T>(items, pageIndex, pageSize, source);
        }
    }
}
