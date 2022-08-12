using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.Extensions.Options;

namespace Mango.Web.Services;

public class ProductService : BaseService, IProductService
{
    private readonly ProductServiceOptions _options;

    public ProductService(IHttpClientFactory httpClientFactory, IOptions<ProductServiceOptions> options) :
        base(httpClientFactory)
    {
        _options = options.Value;
    }

    public async Task<T> GetAllProductsAsync<T>()
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = HttpMethod.Get,
            Url = $"{_options.BaseUrl}/products",
            AccessToken = "",
        });
    }

    public async Task<T> GetProductByIdAsync<T>(int id)
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = HttpMethod.Get,
            Url = $"{_options.BaseUrl}/products/{id}",
            AccessToken = "",
        });
    }

    public async Task<T> CreateProductAsync<T>(ProductDto productDto)
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = HttpMethod.Post,
            Url = $"{_options.BaseUrl}/products",
            Data = productDto,
            AccessToken = "",
        });
    }

    public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = HttpMethod.Put,
            Url = $"{_options.BaseUrl}/products",
            Data = productDto,
            AccessToken = "",
        });
    }

    public async Task<T> DeleteProductAsync<T>(int id)
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = HttpMethod.Delete,
            Url = $"{_options.BaseUrl}/products/{id}",
            AccessToken = "",
        });
    }
}