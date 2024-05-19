using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
	[NotMapped]
	public class Category
	{
		public int Id { get; set; }
		public string? CategoryName { get; set; }
		public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
