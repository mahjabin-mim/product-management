using Microsoft.AspNetCore.Mvc;
using ProductValidation.Services;
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
            // 1. Call the service to validate and generate token
            var token = _authService.Login(loginDto.Username, loginDto.Password);

            // 2. If service returns null, credentials were wrong
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // 3. Return the token to the client
            return Ok(new 
            { 
                token = token,
                expiration = DateTime.Now.AddMinutes(60) // Matches your config
            });
        }
    }
}