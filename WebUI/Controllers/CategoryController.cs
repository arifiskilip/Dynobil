using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyfService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, INotyfService notyfService, IMapper mapper)
        {
            _categoryService = categoryService;
            _notyfService = notyfService;
            _mapper = mapper;
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
                await _categoryService.AddAsync(_mapper.Map<Category>(model));
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
            CategoryUpdateModel model = _mapper.Map<CategoryUpdateModel>(category);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = await _categoryService.GetByIdAsync(model.Id);
                await _categoryService.UpdateAsync(_mapper.Map(model,category));

                _notyfService.Success("Güncelleme işlemi başarılı!");
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
