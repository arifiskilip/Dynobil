using AutoMapper;
using Business.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, NorthwindContext context, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var totalCustomers = await _userManager.Users.CountAsync();
            var totalRoles = await _roleManager.Roles.CountAsync();
            var totalProducts = await _context.Products.CountAsync();
            var totalCategories = await _context.Categories.CountAsync();
            var query = await _context.Products.GroupBy(x => x.CategoryId).Select(x => new CategoryIdByProducts
            {
                CategoryId = x.Key,
                Products = x.Count()
            }).ToListAsync();
            var userClaims = User.FindFirst(ClaimTypes.NameIdentifier);
            return View(new DashboardModel()
            {
                TotalCategories = totalCategories,
                TotalCustomers = totalCustomers,
                TotalProducts = totalProducts,
                TotalRoles = totalRoles
            });
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<User>(model);
                IdentityResult? result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var getUser = await _userManager.FindByEmailAsync(model.Email);
                    var getRole = await _roleManager.FindByIdAsync(((int)RoleEnum.Admin).ToString());
                    await _userManager.AddToRoleAsync(getUser,getRole.Name);
                    return RedirectToAction("Index");
                }
                result.Errors.ToList().ForEach(err =>
                {
                    ModelState.AddModelError(err.Code, err.Description);
                });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = await _userManager.FindByEmailAsync(model.Email);
                if (checkEmail is not null)
                {
                    var checkPassword =  await _signInManager.PasswordSignInAsync(checkEmail, model.Password,model.RemberMe,false);
                    if (checkPassword.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
