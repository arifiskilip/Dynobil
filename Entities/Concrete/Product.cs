using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
	[NotMapped]
	public class Product
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public int CategoryId { get; set; }
		public double UnitPrice { get; set; }


        public Category Category { get; set; }
    }
}
