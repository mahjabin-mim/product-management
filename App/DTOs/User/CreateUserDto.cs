using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        public required string Username { get; set; }
        [Required]  
        public required string Password { get; set; }
        [Required]
        public required string Role { get; set; }
    }  
}