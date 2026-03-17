using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Product
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(20)]
        public required string Name { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]      
        public required int Stock { get; set; }

        [Required]
        [MaxLength(20)]
        public string? CategoryName { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Brand { get; set; }
    }
}