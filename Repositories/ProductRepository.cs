using ProductValidation.Data;
using ProductValidation.Models;
using Microsoft.EntityFrameworkCore;
using ProductValidation.Repositories.Interfaces;
using ProductValidation.DTOs.Product;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ProductValidation.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products
                .Include(p => p.Category) // eager loading
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Create(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetProductsInRange(decimal minPrice, decimal maxPrice)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }

        public IQueryable<ReadProductDto> GetProducts()
        {
            return _dbContext.Products.AsQueryable().ProjectTo<ReadProductDto>(_mapper.ConfigurationProvider);
        }

    }
}