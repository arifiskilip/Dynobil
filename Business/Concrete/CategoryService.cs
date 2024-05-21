using Business.Abstract;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await DuplicateNameCheckAsync(category.CategoryName);
            return await _categoryDal.AddAsync(
                entity: category);
        }

        public async Task DeleteAsync(int id)
        {
            var checkCategory = await _categoryDal.GetAsync(
                predicate: x => x.Id == id,
                enableTracking: true);

            await IsSelectedCategoryAvailable(checkCategory);
            await _categoryDal.DeleteAsync(checkCategory);
        }

        public async Task<IPaginate<Category>> GetAllAsync(int index = 1, int size = 10)
        {
            var categories = await _categoryDal.GetListAsync
                (
                    orderBy: x => x.OrderByDescending(x => x.Id),
                    index: index,
                    size: size
                );
            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var checkCategory = await _categoryDal.GetAsync(
           predicate: x => x.Id == id);
            await IsSelectedCategoryAvailable(checkCategory);
            return checkCategory;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            await IsSelectedCategoryAvailable(category);
            await UpdateDuplicateNameCheckAsync(category.CategoryName, category.Id);
            await _categoryDal.UpdateAsync(category);
            return category;
        }
        public async Task<List<Category>> GetAllNoPagingAsync()
        {
            return await _categoryDal.GetAllNoPagingAsync();
        }

        //Rules
        private async Task DuplicateNameCheckAsync(string name)
        {
            var check = await _categoryDal
                .AnyAsync(x => x.CategoryName.ToLower() == name.ToLower());
            if (check)
            {
                throw new BusinessException("Bu kategori adı zaten mevcut!");
            }
        }

        private async Task UpdateDuplicateNameCheckAsync(string name, int id)
        {
            var check = await _categoryDal
                .GetAsync(x => x.CategoryName.ToLower() == name.ToLower());
            if (check != null && check.Id != id)
            {
                throw new BusinessException("Bu kategori adı zaten mevcut!");
            }
        }

        private async Task IsSelectedCategoryAvailable(Category? category)
        {
            if (category is null) throw new BusinessException("Bu kategori mevcut değil!");
        }


    }
}
