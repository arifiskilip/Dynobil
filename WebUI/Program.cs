using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Business;
using Core.CrossCuttingConcers.Exceptions.Extensions;
using DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//// Add authentication services
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
//        options.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Home/Logout");
//        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/AccessDenied");
//    });

//// Add authorization services
//builder.Services.AddAuthorization();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDataAccessServices();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login"; // Giriþ sayfasý yönlendirme ayarý
    options.AccessDeniedPath = "/Home/AccessDenied"; // Yetki yoksa yönlendirme ayarý
});
builder.Services.AddBusinessServices();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.ConfigureCustomExceptionMiddleware();
app.UseNotyf();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
