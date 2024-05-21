using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class CategoryAddModel
    {

        [Required(ErrorMessage = "Zorunlu alan!")]
        [MinLength(3, ErrorMessage = $"En az 3 karater olmalı!")]
        [MaxLength(20, ErrorMessage = $"En çok 20 karater olmalı!")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!")]
        [MinLength(3, ErrorMessage = $"En az 3 karater olmalı!")]
        [MaxLength(20, ErrorMessage = $"En çok 20 karater olmalı!")]
        public string Description { get; set; }
    }
}
