using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductValidation.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;

        // Dummy users list
        private readonly List<(string Username, string Password)> _dummyUsers = new()
        {
            ("admin", "1234"),
        };

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string Login(string username, string password)
        {
            // Check against dummy users
            var userExists = _dummyUsers.Any(u => u.Username == username && u.Password == password);
            if (!userExists)
                return null;

            // Claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}