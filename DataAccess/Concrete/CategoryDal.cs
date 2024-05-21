using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class CategoryDal : RepositoryBase<Category, int, NorthwindContext>, ICategoryDal
    {
        public CategoryDal(NorthwindContext context) : base(context)
        {
        }
    }
}
