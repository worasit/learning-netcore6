using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.DbContexts.Models;
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

    public async Task<ProductDto> CreateProduct(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var createdProduct = await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task<bool> UpdateProduct(ProductDto productDto)
    {
        if (productDto.ProductId <= 0)
        {
            return false;
        }

        var product = _mapper.Map<Product>(productDto);
        _context.Update(product);
        return await _context.SaveChangesAsync() >= 1;
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.ProductId.Equals(productId));
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}