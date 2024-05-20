using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class CategoryAddModel
    {

        //[Required(ErrorMessage = "Zorunlu alan!")]
        //[MinLength(1, ErrorMessage = "Az karakter")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Zorunlu alan!")]
        //[MinLength(1, ErrorMessage = "Az karakter")]
        public string Description { get; set; }
    }
}
