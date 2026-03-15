using ProductValidation.Models;
using ProductValidation.DTOs;
using AutoMapper;
using ProductValidation.DTOs.Product;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, CreateUserDto>();
        CreateMap<CreateUserDto, Product>();
        CreateMap<Product, UpdateProductDto>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, ReadProductDto>();
        CreateMap<ReadProductDto, Product>();
    }
}