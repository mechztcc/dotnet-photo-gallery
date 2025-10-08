using System.ComponentModel.DataAnnotations;


namespace AppApi.Modules.Users.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is invalid")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be contain min 6 characters")]
        public required string Password { get; set; }
    }

}