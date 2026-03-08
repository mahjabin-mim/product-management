using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Product
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }

        public required decimal Price { get; set; }

        public required int Stock { get; set; }

        public required string Category { get; set; }

        public required string Brand { get; set; }
    }
}