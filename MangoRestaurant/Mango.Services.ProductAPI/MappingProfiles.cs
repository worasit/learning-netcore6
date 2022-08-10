using AutoMapper;
using Mango.Services.ProductAPI.DbContexts.Models;
using Mango.Services.ProductAPI.DbContexts.Models.Dto;

namespace Mango.Services.ProductAPI;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<ProductDto, Product>().ReverseMap();
    }
}