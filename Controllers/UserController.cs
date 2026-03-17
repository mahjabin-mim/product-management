using Microsoft.AspNetCore.Mvc;
using ProductValidation.DTOs.User;
using ProductValidation.Services.Interfaces;

namespace ProductValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserSetService _setService;
        public UserController(IUserSetService setService)
        {
            _setService = setService;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            var user = await _setService.CreateUserService(createUserDto);
            return Ok(user);
        }
    }
}