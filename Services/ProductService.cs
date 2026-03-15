using ProductValidation.Services.Interfaces;
using ProductValidation.DTOs.Product;
using ProductValidation.Models; 
using ProductValidation.Repositories.Interfaces;
using ProductValidation.Helpers;
using AutoMapper;

namespace ProductValidation.Services
{
    public class ProductService : IProductGetService, IProductSetService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IMapper mapper)
         {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<ReadProductDto> GetAllService()
        {
            var productList = _productRepository.GetAll()
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
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                CategoryId = createProductDto.CategoryId,
                Brand = createProductDto.Brand
            };

            _productRepository.Create(newProduct);

            _logger.LogInformation(
                "Product {ProductName} created at {TimeStamp}",
                newProduct.Name,
                DateTime.UtcNow
            );
            
            return newProduct;   
        }

        public Product? UpdateProductService(int id, UpdateProductDto updateProductDto)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                product.Name = updateProductDto.Name;
                product.Price = updateProductDto.Price;
                product.Stock = updateProductDto.Stock;
                product.CategoryId = updateProductDto.CategoryId;
                product.Brand = updateProductDto.Brand;

                _productRepository.Update(product);
                return product;
            }
            else
            {
                return null; 
            }
        }

        public bool DeleteProductService(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(id);
                return true;
            }
            else
            {
                return false;
            }
        }

         public IEnumerable<ReadProductDto> GetProductInRangeService(decimal minPrice, decimal maxPrice)
        {
            var productList =  _productRepository.GetProductsInRange(minPrice, maxPrice)
                .Select(p => new ReadProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryName = p.Category?.Name, // lazy loading
                    Brand = p.Brand
                });

                return productList;
        }

        public PageResponse<Product> GetProducts(QueryParams queryParams)
        {
            var query = _productRepository.GetProducts();

            return QueryHelper.ApplyQuery(query, queryParams);
        }

    }
}