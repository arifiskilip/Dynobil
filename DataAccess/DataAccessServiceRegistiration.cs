using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
	public static class DataAccessServiceRegistiration
	{
		public static void AddDataAccessServices(this IServiceCollection services)
		{
			services.AddDbContext<NorthwindContext>(opt =>
			{
				opt.UseNpgsql("User Id=postgres;Password=123;Server=localhost;Port=5432;Database=Northwind;");
			});

			services.AddIdentity<User, Role>(opt =>
			{
				
			}).AddEntityFrameworkStores<NorthwindContext>()
			.AddDefaultTokenProviders();
		}	
	}
}
