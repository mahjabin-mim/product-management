using ProductValidation.Services.Interfaces;
using ProductValidation.DTOs.Product;
using ProductValidation.Models; 
using ProductValidation.Data;

namespace ProductValidation.Services
{
    public class ProductService : IProductGetService
    {
        private readonly AppDbContext dbContext;
        public ProductService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Product> getAllService()
        {
            return dbContext.Products.ToList();      
        }

        public Product CreateProductService(CreateProductDto createProductDto)
        {
            var newProduct = new Product
            {
                Id = new Random().Next(1, 10),
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                Category = createProductDto.Category,
                Brand = createProductDto.Brand
            };

            dbContext.Products.Add(newProduct);   
            dbContext.SaveChanges();             
            return newProduct;   
        }

        // public bool UpdateProductService(UpdateProductDto updateProductDto)
        // {
        //     var product = 
        // }

        // public bool DeleteProductService(int id)
        // {
        //     var product = 
        // }
        
    }
}