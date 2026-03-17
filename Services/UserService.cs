using ProductValidation.Models;
using ProductValidation.DTOs.User;
using ProductValidation.Repositories.Interfaces;
using ProductValidation.Services.Interfaces;
using ProductValidation.Enums;

namespace ProductValidation.Services
{
    public class UserService : IUserSetService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> CreateUserService(CreateUserDto createUserDto)
        {
            var newUser = new User
            {
                Username = createUserDto.Username,
                Password = createUserDto.Password,
                Role = Enum.Parse<UserRole>(createUserDto.Role)
            };

            await _userRepository.Create(newUser);
            return newUser;
        }
    }
}