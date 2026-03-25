using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductValidation.Services;
using ProductValidation.Repositories.Interfaces;
using ProductValidation.DTOs.Product;
using ProductValidation.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ProductValidation.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<ICategoryRepository> _categoryRepoMock;
        private readonly Mock<ILogger<ProductService>> _loggerMock;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _categoryRepoMock = new Mock<ICategoryRepository>();
            _loggerMock = new Mock<ILogger<ProductService>>();

            // AutoMapper config
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProductDto, Product>();
                cfg.CreateMap<Product, ReadProductDto>();
                cfg.CreateMap<UpdateProductDto, Product>();
            });

            _mapper = config.CreateMapper();

            _productService = new ProductService(
                _productRepoMock.Object,
                _categoryRepoMock.Object,
                _loggerMock.Object,
                _mapper
            );
        }

        // ================= CREATE PRODUCT =================
        [Fact]
        public async Task CreateProductService_ShouldThrowException_WhenCategoryNameIsNull()
        {
            var dto = new CreateProductDto
            {
                Name = "Test",
                Price = 100,
                Stock = 5,
                Brand = "Brand1",
                CategoryName = null
            };

            await Assert.ThrowsAsync<Exception>(() =>
                _productService.CreateProductService(dto));
        }

        [Fact]
        public async Task CreateProductService_ShouldThrowException_WhenCategoryNotFound()
        {
            var dto = new CreateProductDto
            {
                Name = "Test",
                Price = 100,
                Stock = 5,
                Brand = "Brand1",
                CategoryName = "Electronics"
            };

            _categoryRepoMock.Setup(x => x.GetByName("Electronics"))
                             .ReturnsAsync((Category?)null);

            await Assert.ThrowsAsync<Exception>(() =>
                _productService.CreateProductService(dto));
        }

        [Fact]
        public async Task CreateProductService_ShouldReturnProduct_WhenValid()
        {
            // Arrange: DTO for creating product
            var dto = new CreateProductDto
            {
                Name = "Laptop",
                Price = 1500,
                Stock = 5,
                Brand = "BrandX",
                CategoryName = "Electronics"
            };

            // Arrange: existing category
            var category = new Category
            {
                Id = 1,
                Name = "Electronics"
            };

            // Mock the category repository to return the category
            _categoryRepoMock.Setup(x => x.GetByName("Electronics"))
                            .ReturnsAsync(category);

            // Mock the product repository Create method
            _productRepoMock.Setup(x => x.Create(It.IsAny<Product>()))
                            .ReturnsAsync((Product p) => p);

            // Act: call the service
            var result = await _productService.CreateProductService(dto);

            // Assert: verify the returned product DTO
            Assert.NotNull(result);
            Assert.Equal("Laptop", result.Name);
            Assert.Equal(1500, result.Price);
            Assert.Equal(5, result.Stock);
            Assert.Equal("BrandX", result.Brand);
            Assert.Equal("Electronics", result.CategoryName);

            // Optional: verify that Create was called once
            _productRepoMock.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
        }

        // ================= GET ALL PRODUCTS =================
        [Fact]
        public async Task GetAllService_ShouldReturnAllProducts()
        {
            var products = new List<Product>
            {
                new Product { Name = "P1", Price = 100, Stock=5, Brand="B1", Category=new Category { Name = "C1" } },
                new Product { Name = "P2", Price = 200, Stock=3, Brand="B2", Category=new Category { Name = "C2" } }
            };

            _productRepoMock.Setup(x => x.GetAll()).ReturnsAsync(products);

            var result = await _productService.GetAllService();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Name == "P1");
            Assert.Contains(result, p => p.Name == "P2");
        }

        // ================= UPDATE PRODUCT =================
        [Fact]
        public async Task UpdateProductService_ShouldReturnUpdatedProduct_WhenProductExists()
        {
            var product = new Product
            {
                Id = 1,
                Name = "OldName",
                Price = 100,
                Stock = 5,
                Brand = "OldBrand",
                Category = new Category { Name = "C1" }
            };

            var updateDto = new UpdateProductDto
            {
                Name = "NewName",
                Price = 150,
                Stock = 10,
                Brand = "NewBrand",
                CategoryName = "C1"
            };

            _productRepoMock.Setup(x => x.GetById(1)).ReturnsAsync(product);
            _productRepoMock.Setup(x => x.Update(It.IsAny<Product>())).ReturnsAsync((Product p) => p);

            var result = await _productService.UpdateProductService(1, updateDto);

            Assert.NotNull(result);
            Assert.Equal("NewName", result.Name);
            Assert.Equal(150, result.Price);
            Assert.Equal(10, result.Stock);
            Assert.Equal("NewBrand", result.Brand);
        }

        [Fact]
        public async Task UpdateProductService_ShouldReturnNull_WhenProductDoesNotExist()
        {
            var updateDto = new UpdateProductDto
            {
                Name = "NewName",
                Price = 150,
                Stock = 5,
                Brand = "NewBrand",
                CategoryName = "C1"
            };

            _productRepoMock.Setup(x => x.GetById(1)).ReturnsAsync((Product?)null);

            var result = await _productService.UpdateProductService(1, updateDto);

            Assert.Null(result);
        }

        // ================= DELETE PRODUCT =================
        [Fact]
        public async Task DeleteProductService_ShouldReturnTrue_WhenProductExists()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Price = 100,
                Stock = 5,
                Brand = "Brand1",
                Category = new Category { Name = "C1" }
            };

            _productRepoMock.Setup(x => x.GetById(1)).ReturnsAsync(product);
            _productRepoMock.Setup(x => x.Delete(1)).ReturnsAsync(true);

            var result = await _productService.DeleteProductService(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProductService_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            _productRepoMock.Setup(x => x.GetById(1)).ReturnsAsync((Product?)null);

            var result = await _productService.DeleteProductService(1);

            Assert.False(result);
        }

        // ================= GET PRODUCTS IN RANGE =================
        [Fact]
        public async Task GetProductInRangeService_ShouldReturnProductsInRange()
        {
            var products = new List<Product>
            {
                new Product { Name = "P1", Price = 100, Stock=5, Brand="B1", Category=new Category { Name = "C1" } },
                new Product { Name = "P2", Price = 200, Stock=3, Brand="B2", Category=new Category { Name = "C2" } }
            };

            _productRepoMock.Setup(x => x.GetProductsInRange(50, 250)).ReturnsAsync(products);

            var result = await _productService.GetProductInRangeService(50, 250);

            Assert.Equal(2, result.Count());
            Assert.All(result, p => Assert.InRange(p.Price, 50, 250));
        }
    }
}