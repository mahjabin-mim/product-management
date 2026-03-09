using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Product
{
    public class ReadProductDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? CategoryName { get; set; }
    }
}