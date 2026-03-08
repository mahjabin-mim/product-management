using Microsoft.AspNetCore.Mvc;
using ProductValidation.DTOs.Product;
using ProductValidation.Models;
using ProductValidation.Services.Interfaces;
using ProductValidation.Services;

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
            var productList = getService.getAllService();
            return Ok(productList);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateProductDto createProductDto)
        {
            var product = setService.CreateProductService(createProductDto);
            return Ok(product);
        }

        // [HttpPut("update")]
        // public IActionResult Update(UpdateProductDto updateProductDto)
        // {
        //     var product = setService.UpdateProductService(updateProductDto);
        // }
    }
}