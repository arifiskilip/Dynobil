using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [NotMapped]
	public class Category : Entity
	{
		public int Id { get; set; }
		public string? CategoryName { get; set; }
		public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
