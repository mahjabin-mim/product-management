using ProductValidation.Services.Interfaces;
using ProductValidation.DTOs.Product;
using ProductValidation.Models; 
using ProductValidation.Data;
using Microsoft.EntityFrameworkCore;
using ProductValidation.Repositories;

namespace ProductValidation.Services
{
    public class ProductService : IProductGetService, IProductSetService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IEnumerable<ReadProductDto> GetAllService()
        {
            var productList = productRepository.GetAll()
            .Select(p => new ReadProductDto
            {
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Name, 
                Brand = p.Brand
            });

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
                CategoryId = createProductDto.CategoryId,
                Brand = createProductDto.Brand
            };

            productRepository.Create(newProduct);
            return newProduct;   
        }

        public Product? UpdateProductService(int id, UpdateProductDto updateProductDto)
        {
            var product = productRepository.GetById(id);
            if (product != null)
            {
                product.Name = updateProductDto.Name;
                product.Price = updateProductDto.Price;
                product.Stock = updateProductDto.Stock;
                product.CategoryId = updateProductDto.CategoryId;
                product.Brand = updateProductDto.Brand;

                productRepository.Update(product);
                return product;
            }
            else
            {
                return null; 
            }
        }

        public bool DeleteProductService(int id)
        {
            var product = productRepository.GetById(id);
            if (product != null)
            {
                productRepository.Delete(id);
                return true;
            }
            else
            {
                return false;
            }
        }

         public IEnumerable<ReadProductDto> GetProductInRangeService(decimal minPrice, decimal maxPrice)
        {
            var productList = productRepository.GetProductsInRange(minPrice, maxPrice)
                .Select(p => new ReadProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryName = p.Category.Name, // lazy loading
                    Brand = p.Brand
                });

                return productList;
        }
    }
}