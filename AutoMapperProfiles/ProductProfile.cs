using AutoMapper;
using ProductValidation.DTOs.Product;
using ProductValidation.Models;

namespace ProductValidation.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, CreateUserDto>();
            CreateMap<CreateUserDto, Product>();
            CreateMap<Product, UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, ReadProductDto>();
            CreateMap<ReadProductDto, Product>();
        }
    }
}