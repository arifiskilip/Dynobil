using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Paging;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index(int index=1)
        {
            ViewBag.index=index;
            var categories = Paginate<Category>.Create(_context.Categories, index, 5);
            return View(categories);
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            return View();
        }
    }
}
