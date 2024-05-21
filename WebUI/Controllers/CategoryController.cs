using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyfService;

        public CategoryController(ICategoryService categoryService, INotyfService notyfService)
        {
            _categoryService = categoryService;
            _notyfService = notyfService;
        }

        public async Task<IActionResult> Index(int index=1, int size=10)
        {
            var categories = await _categoryService.GetAllAsync(index, size);
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(new()
                {
                    CategoryName = model.Name,
                    Description = model.Description,
                });
                _notyfService.Success("Ekleme işlemi başarılı");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id=0)
        {
            await _categoryService.DeleteAsync(id);
            _notyfService.Success("Silme işlemi başarılı");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id=0)
        {
            Category category = await _categoryService.GetByIdAsync(id);
            CategoryUpdateModel model = new CategoryUpdateModel()
            {
                Id = category.Id,
                Description = category.Description,
                Name = category.CategoryName
            };
            return View(model);
        }
    }
}
