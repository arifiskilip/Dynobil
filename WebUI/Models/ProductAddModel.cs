using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class ProductAddModel
    {
        [Required(ErrorMessage = "Zorunlu alan!")]
        [MinLength(3, ErrorMessage = $"En az 3 karater olmalı!")]
        [MaxLength(20, ErrorMessage = $"En çok 20 karater olmalı!")]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
        [Required(ErrorMessage = "Zorunlu alan!")]
        [Range(1, 1000, ErrorMessage = "Ürün fiyatı 1-1000 arasında olmalı!")]
        public double UnitPrice { get; set; }
    }
}
