using Microsoft.AspNetCore.Mvc;
using ProductValidation.DTOs.Product;
using ProductValidation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ProductValidation.Helpers;

namespace ProductValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize (Roles = "Admin,User")]
    public class ProductController : ControllerBase
    {
       private readonly IProductGetService _getService;
       private readonly IProductSetService _setService;
       
       public ProductController(IProductGetService getService, IProductSetService setService)
       {
            _getService = getService;
            _setService = setService;
       }

        [AllowAnonymous]
       [HttpGet("getall")]
       public async Task<IActionResult> GetALL()
        {
            var productList = await _getService.GetAllService();
            return Ok(productList);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateProductDto createProductDto)
        {
            var product = await _setService.CreateProductService(createProductDto);
            return Ok(product);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateProductDto updateProductDto)
        {
            var product = await _setService.UpdateProductService(id, updateProductDto);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _setService.DeleteProductService(id);
            return Ok(product);
        }

        [AllowAnonymous]
        [HttpGet("getbyrange")]
        public async Task<IActionResult> GetByRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var productList = await _getService.GetProductInRangeService(minPrice, maxPrice);
            return Ok(productList);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] QueryParams queryParams)
        {
            var result = await _getService.GetProducts(queryParams);
            return Ok(result);
        }

    }
}