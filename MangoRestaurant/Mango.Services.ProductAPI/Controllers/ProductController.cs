using Mango.Services.ProductAPI.DbContexts.Models.Dto;
using Mango.Services.ProductAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ResponseDto _response;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                var productDtos = await _productRepository.GetProducts();
                _response.Result = productDtos;
            }
            catch (Exception e)
            {
                _response.isSuccess = false;
                _response.ErrorMessage = new List<string> {e.ToString()};
            }

            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var productDto = await _productRepository.GetProductById(id);
                _response.Result = productDto;
            }
            catch (Exception e)
            {
                _response.isSuccess = false;
                _response.ErrorMessage = new List<string> {e.ToString()};
            }

            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] ProductDto productDto)
        {
            try
            {
                var createdProduct = await _productRepository.CreateProduct(productDto);
                _response.Result = createdProduct;
            }
            catch (Exception e)
            {
                _response.isSuccess = false;
                _response.ErrorMessage = new List<string> {e.ToString()};
            }

            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var updatedProduct = await _productRepository.UpdateProduct(productDto);
                _response.Result = updatedProduct;
            }
            catch (Exception e)
            {
                _response.isSuccess = false;
                _response.ErrorMessage = new List<string> {e.ToString()};
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var isSuccess = await _productRepository.DeleteProduct(id);
                _response.Result = isSuccess;
            }
            catch (Exception e)
            {
                _response.isSuccess = false;
                _response.ErrorMessage = new List<string> {e.ToString()};
            }

            return _response;
        }
    }
}