using ProductValidation.Services.Interfaces;
using ProductValidation.DTOs.Product;
using ProductValidation.Models; 
using ProductValidation.Repositories.Interfaces;
using ProductValidation.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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

        public async Task<IEnumerable<ReadProductDto>> GetAllService()
        {
            var productList = await _productRepository.GetAll();
                // Manual mapping
                // .Select(p => new ReadProductDto
                // {
                //     Name = p.Name,
                //     Price = p.Price,
                //     Stock = p.Stock,
                //     CategoryName = p.Category.Name, 
                //     Brand = p.Brand
                // });

            return _mapper.Map<IEnumerable<ReadProductDto>>(productList);;      
        }       

        public async Task<Product> CreateProductService(CreateProductDto createProductDto)
        {
            // Manual mapping
            // var newProduct = new Product
            // {
            //     Name = createProductDto.Name,
            //     Price = createProductDto.Price,
            //     Stock = createProductDto.Stock,
            //     CategoryId = createProductDto.CategoryId,
            //     Brand = createProductDto.Brand
            // };

            var newProduct = _mapper.Map<Product>(createProductDto);

            await _productRepository.Create(newProduct);

            _logger.LogInformation(
                "Product {ProductName} created at {TimeStamp}",
                newProduct.Name,
                DateTime.UtcNow
            );
            
            return newProduct;   
        }

        public async Task<Product?> UpdateProductService(int id, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetById(id);
            if (product != null)
            {
                _mapper.Map(updateProductDto, product);

                await _productRepository.Update(product);
                return product;
            }
            else
            {
                return null; 
            }
        }

        public async Task<bool> DeleteProductService(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product != null)
            {
                await _productRepository.Delete(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<ReadProductDto>> GetProductInRangeService(decimal minPrice, decimal maxPrice)
        {
            var productList = await _productRepository.GetProductsInRange(minPrice, maxPrice);
            return productList.Select(p => new ReadProductDto
            {
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category?.Name, // lazy loading
                Brand = p.Brand
            });

        }

        // Pagination, Sorting, Filtering, Searching, AutoMapper Projection
        public async Task<PageResponse<ReadProductDto>> GetProducts(QueryParams queryParams)
        {
            var query = _productRepository.GetProducts()
                .ProjectTo<ReadProductDto>(_mapper.ConfigurationProvider);

            return await QueryHelper.ApplyQuery(query, queryParams);
        }

    }
}