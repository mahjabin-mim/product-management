using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Product
{
    public class ReadProductDto
    {
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? Category { get; set; }

        public string? Brand { get; set; }
    }
}