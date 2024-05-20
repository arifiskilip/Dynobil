using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebUI.Models;
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

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddModel model)
        {

                //await _context.Categories.AddAsync(new()
                //{
                //    CategoryName = model.Name,
                //    Description = model.Description
                //});
                //await _context.SaveChangesAsync();
                var data = JsonConvert.SerializeObject(model);
                return Json(new { success = true, message = "Ekleme işlemi başarılı!", data= data });

        }
    }
}
