using ProductValidation.Models;
using ProductValidation.Repositories.Interfaces;
using ProductValidation.Services.Interfaces;
using ProductValidation.Enums;

namespace ProductValidation.Services
{
    public class UserService : IUserSetService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User CreateUserService(CreateUserDto createUserDto)
        {
            var newUser = new User
            {
                Username = createUserDto.Username,
                Password = createUserDto.Password,
                Role = Enum.Parse<UserRole>(createUserDto.Role)
            };

            return userRepository.Create(newUser);
        }
    }
}