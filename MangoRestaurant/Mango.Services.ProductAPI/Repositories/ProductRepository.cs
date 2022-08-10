using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.DbContexts.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private IMapper _mapper;

    public ProductRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductById(int productId)
    {
        var product = await _context.Products.Where(product => product.ProductId.Equals(product)).FirstOrDefaultAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }
}