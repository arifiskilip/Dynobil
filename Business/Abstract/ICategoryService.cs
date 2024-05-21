using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IPaginate<Category>> GetAllAsync(int index = 1, int size = 10);
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
