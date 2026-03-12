using ProductValidation.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProductValidation.Models;

namespace ProductValidation.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext dbContext;

        public AuthService(IConfiguration config, AppDbContext dbContext)
        {
            _config = config;
            this.dbContext = dbContext;
        }

        public string Login(string username, string password)
        {
            var user = dbContext.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
                return null;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds
            );
    
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}