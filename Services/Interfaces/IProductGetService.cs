using ProductValidation.Models;
using ProductValidation.Services.Interfaces;

namespace ProductValidation.Services.Interfaces
{
    public interface IProductGetService
    {
        IEnumerable<Product> getAllService();
    }
}