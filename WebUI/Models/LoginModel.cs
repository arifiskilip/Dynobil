using System.ComponentModel.DataAnnotations;

namespace WebUI.Models;

public class LoginModel
{
    [Required(ErrorMessage ="E mail zorunlu!")]
    [EmailAddress(ErrorMessage ="E mail standartları")
    [MinLength(5,ErrorMessage ="E mail zorunlu!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Zorunlu alan!")]
    public string Password { get; set; }
    public bool RemberMe { get; set; } = false;
}
