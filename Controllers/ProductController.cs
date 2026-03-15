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
       public IActionResult GetALL()
        {
            var productList = _getService.GetAllService();
            return Ok(productList);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateProductDto createProductDto)
        {
            var product = _setService.CreateProductService(createProductDto);
            return Ok(product);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody]UpdateProductDto updateProductDto)
        {
            var product = _setService.UpdateProductService(id, updateProductDto);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var product = _setService.DeleteProductService(id);
            return Ok(product);
        }

        [HttpGet("getbyrange")]
        public IActionResult GetByRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var productList = _getService.GetProductInRangeService(minPrice, maxPrice);
            return Ok(productList);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetProducts([FromQuery] QueryParams queryParams)
        {
            var result = _getService.GetProducts(queryParams);
            return Ok(result);
        }

    }
}