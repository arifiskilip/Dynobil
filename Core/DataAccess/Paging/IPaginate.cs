using Core.Entities;

namespace Core.DataAccess.Paging
{
    public interface IPaginate<T>
    {
        public IQueryable<T> Items { get; }
        public PaginationInfo Pagination { get; }
    }
}
