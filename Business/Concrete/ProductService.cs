using Business.Abstract;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await DuplicateNameCheckAsync(product.ProductName);
            return await _productDal.AddAsync(
                entity: product,
                includeProperties: x => x.Category);
        }

        public async Task DeleteAsync(int id)
        {
            var checkProduct = await _productDal.GetAsync(
                predicate: x => x.Id == id,
                enableTracking: true);

            await IsSelectedProductAvailable(checkProduct);
            await _productDal.DeleteAsync(checkProduct);
        }

        public async Task<IPaginate<Product>> GetAllAsync(int index = 1, int size = 10)
        {
            var products = await _productDal.GetListAsync
                (
                    include: x => x.Include(x => x.Category),
                    orderBy: x => x.OrderByDescending(x => x.Id),
                    index: index,
                    size: size
                );
            return products;

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var checkProduct = await _productDal.GetAsync(
                predicate: x => x.Id == id,
                include: x => x.Include(x => x.Category));
            await IsSelectedProductAvailable(checkProduct);
            return checkProduct;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var checkProduct = await _productDal.GetAsync(
                predicate: x => x.Id == product.Id);
            await IsSelectedProductAvailable(checkProduct);
            await UpdateDuplicateNameCheckAsync(product.ProductName, checkProduct.Id);
            await _productDal.UpdateAsync(product);
            return product;
        }


        //Rules 

        private async Task DuplicateNameCheckAsync(string name)
        {
            var check = await _productDal
                .AnyAsync(x => x.ProductName.ToLower() == name.ToLower());
            if (check)
            {
                throw new BusinessException("Bu ürün adı zaten mevcut!");
            }
        }

        private async Task UpdateDuplicateNameCheckAsync(string name, int id)
        {
            var check = await _productDal
                .GetAsync(x => x.ProductName.ToLower() == name.ToLower());
            if (check != null && check.Id != id)
            {
                throw new BusinessException("Bu ürün adı zaten mevcut!");
            }
        }

        private async Task IsSelectedProductAvailable(Product? product)
        {
            if (product is null) throw new BusinessException("Bu ürün mevcut değil!");
        }
    }
}
