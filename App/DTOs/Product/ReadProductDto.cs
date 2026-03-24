using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Product
{
    public class ReadProductDto
    {
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Brand { get; set; }

        [Required]
        [MaxLength(20)]
        public string? CategoryName { get; set; }
    }
}