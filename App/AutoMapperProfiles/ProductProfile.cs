using AutoMapper;
using ProductValidation.DTOs.Product;
using ProductValidation.Models;

namespace ProductValidation.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, ReadProductDto>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}