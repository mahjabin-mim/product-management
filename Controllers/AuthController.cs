using Microsoft.AspNetCore.Mvc;
using ProductValidation.Services;
using ProductValidation.DTOs;
using ProductValidation.DTOs.Auth;

namespace ProductValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var token = _authService.Login(loginDto.Username, loginDto.Password);
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            return Ok(new 
            { 
                token = token,
                expiration = DateTime.Now.AddMinutes(60) 
            });
        }
    }
}