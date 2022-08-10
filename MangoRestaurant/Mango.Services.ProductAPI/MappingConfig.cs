using AutoMapper;
using Mango.Services.ProductAPI.DbContexts.Models;
using Mango.Services.ProductAPI.DbContexts.Models.Dto;

namespace Mango.Services.ProductAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();
        });
        return mapperConfiguration;
    }
}