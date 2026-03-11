using Microsoft.AspNetCore.Mvc;
using ProductValidation.DTOs.Product;
using ProductValidation.Models;
using ProductValidation.Services.Interfaces;
using ProductValidation.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProductValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
       private readonly IProductGetService getService;
       private readonly IProductSetService setService;
       
       public ProductController(IProductGetService getService, IProductSetService setService)
       {
            this.getService = getService;
            this.setService = setService;
       }

       [HttpGet("getall")]
       public IActionResult GetALL()
        {
            var productList = getService.GetAllService();
            return Ok(productList);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateProductDto createProductDto)
        {
            var product = setService.CreateProductService(createProductDto);
            return Ok(product);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody]UpdateProductDto updateProductDto)
        {
            var product = setService.UpdateProductService(id, updateProductDto);
            return Ok(product);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var product = setService.DeleteProductService(id);
            return Ok(product);
        }

        [HttpGet("getbyrange")]
        public IActionResult GetByRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var productList = getService.GetProductInRangeService(minPrice, maxPrice);
            return Ok(productList);
        }

        [Authorize]
        [HttpGet("private")]
        public IActionResult PrivateEndpoint()
        {
            return Ok("Only authenticated users can see this");
        }  
    }
}