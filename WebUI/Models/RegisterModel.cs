using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
	public class RegisterModel
	{
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string FirstName { get; set; }
		[Required(ErrorMessage = "Zorunlu alan!")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Zorunlu alan!")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Zorunlu alan!")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "Zorunlu alan!")]
		public string Password { get; set; }
        public bool RemberMe { get; set; }
    }
}
