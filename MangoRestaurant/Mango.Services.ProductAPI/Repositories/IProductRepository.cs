using Mango.Services.ProductAPI.DbContexts.Models.Dto;

namespace Mango.Services.ProductAPI.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProductById(int productId);
    Task<ProductDto> CreateProduct(ProductDto productDto);
    Task<bool> UpdateProduct(ProductDto productDto);
    Task<bool> DeleteProduct(int productId);
}