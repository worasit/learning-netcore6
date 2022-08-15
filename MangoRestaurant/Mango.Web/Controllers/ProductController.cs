using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            var productDtos = new List<ProductDto>();
            var responseDto = await _productService.GetAllProductsAsync<ResponseDto>();
            if (responseDto.isSuccess && responseDto.Result != null)
            {
                productDtos = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Result));
            }

            return View(productDtos);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (!ModelState.IsValid) return View(productDto);
            var responseDto = await _productService.CreateProductAsync<ResponseDto>(productDto);
            if (responseDto != null && responseDto.isSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }

            return View(productDto);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var responseDto = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (!responseDto.isSuccess) return NotFound();
            var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            var responseDto = await _productService.UpdateProductAsync<ResponseDto>(productDto);
            if (responseDto != null && responseDto.isSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }

            return View(productDto);
        }
    }
}