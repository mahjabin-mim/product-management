using System.ComponentModel.DataAnnotations;

namespace ProductValidation.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public required decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        public required int Stock { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public required string Category { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        public required string Brand { get; set; }
    }
}