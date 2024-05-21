using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class ProductDal : RepositoryBase<Product, int, NorthwindContext>, IProductDal
    {
        public ProductDal(NorthwindContext context) : base(context)
        {
        }
    }
}
