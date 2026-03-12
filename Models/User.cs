using System.ComponentModel.DataAnnotations;
using ProductValidation.Enums;

namespace ProductValidation.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; } 
        [Required]
        public required UserRole Role { get; set; }
    }
}