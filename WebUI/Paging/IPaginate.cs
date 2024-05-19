using System.Security.Principal;

namespace WebUI.Paging
{
    public interface IPaginate<T>
          where T : class, new()
    {
        public IQueryable<T> Items { get; }
        public PaginationInfo Pagination { get; }
    }
}
