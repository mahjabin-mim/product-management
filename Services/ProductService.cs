using ProductValidation.Services.Interfaces;
using ProductValidation.DTOs.Product;
using ProductValidation.Models; 
using ProductValidation.Data;
using Microsoft.EntityFrameworkCore;

namespace ProductValidation.Services
{
    public class ProductService : IProductGetService, IProductSetService
    {
        private readonly AppDbContext dbContext;
        public ProductService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<ReadProductDto> getAllService()
        {
            var productList = dbContext.Products.AsNoTracking()
            .Select(p => new ReadProductDto
            {
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Category = p.Category,
                Brand = p.Brand
            })
            .ToList();

            return productList;      
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

        public Product UpdateProductService(int id, UpdateProductDto updateProductDto)
        {
            var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Name = updateProductDto.Name;
                product.Price = updateProductDto.Price;
                product.Stock = updateProductDto.Stock;
                product.Category = updateProductDto.Category;
                product.Brand = updateProductDto.Brand;

                dbContext.SaveChanges();
                return product;
            }
            else
            {
                return null; 
            }
        }

        public bool DeleteProductService(int id)
        {
            var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

         public IEnumerable<ReadProductDto> getProductInRangeService(decimal minPrice, decimal maxPrice)
        {
            var productList = dbContext.Products.AsNoTracking()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .Select(p => new ReadProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Category = p.Category,
                    Brand = p.Brand
                })
                .ToList();

                return productList;
        }
    }
}