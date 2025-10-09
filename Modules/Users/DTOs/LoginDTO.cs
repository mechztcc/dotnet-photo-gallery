using System.ComponentModel.DataAnnotations;


namespace AppApi.Modules.Users.DTOs
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is invalid")]

        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}