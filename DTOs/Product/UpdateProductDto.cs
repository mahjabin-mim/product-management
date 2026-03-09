using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Product
{
    public class UpdateProductDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]      
        public required int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public required string Brand { get; set; }
    }
}