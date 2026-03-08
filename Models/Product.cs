using System.ComponentModel.DataAnnotations;

namespace ProductValidation.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required int Stock { get; set; }

        [Required]
        public required string Category { get; set; }

        [Required]
        public required string Brand { get; set; }
    }
}