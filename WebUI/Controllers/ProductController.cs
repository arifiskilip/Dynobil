using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyfService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, INotyfService notyfService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _notyfService = notyfService;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int index = 1, int size = 10)
        {
            var products = await _productService.GetAllAsync(index, size);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ProductAddModel model = new();
            var categories = await _categoryService.GetAllNoPagingAsync();
            var categoriesSelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList();
            model.Categories = categoriesSelectList;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddModel model)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(_mapper.Map<Product>(model));
                _notyfService.Success("Ekleme işlemi başarılı");
                return RedirectToAction("Index");
            }
            var categories = await _categoryService.GetAllNoPagingAsync();
            var categoriesSelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList();
            model.Categories = categoriesSelectList;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id = 0)
        {
            await _productService.DeleteAsync(id);
            _notyfService.Success("Silme işlemi başarılı");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id = 0)
        {
            var categories = await _categoryService.GetAllNoPagingAsync();
            var categoriesSelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList();
            
            Product product = await _productService.GetByIdAsync(id);
            ProductUpdateModel model = _mapper.Map<ProductUpdateModel>(product);
            model.Categories = categoriesSelectList;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = await _productService.GetByIdAsync(model.Id);
                await _productService.UpdateAsync(_mapper.Map(model, product));

                _notyfService.Success("Güncelleme işlemi başarılı!");
                return RedirectToAction("Index");
            }
            var categories = await _categoryService.GetAllNoPagingAsync();
            var categoriesSelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList();
            model.Categories = categoriesSelectList;
            return View(model);
        }

    }
}
