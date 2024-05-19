using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccess.Contexts
{
	public class NorthwindContext : IdentityDbContext<User, Role, int>
	{
		public NorthwindContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Product>(entity =>
			{
				entity.ToTable("products");
				entity.HasKey(x => x.Id);

				entity.Property(x => x.Id).HasColumnName("product_id");
				entity.Property(x => x.ProductName).HasColumnName("product_name");
				entity.Property(e => e.CategoryId)
			   .HasColumnName("category_id");

				entity.Property(e => e.UnitPrice)
					.HasColumnName("unit_price");
			});

			builder.Entity<Category>(entity =>
			{
				entity.ToTable("categories"); 
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Id)
					.HasColumnName("category_id");

				entity.Property(e => e.CategoryName)
					.HasColumnName("category_name");

				entity.Property(e => e.Description)
					.HasColumnName("description");
			});
			

		}

		public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
