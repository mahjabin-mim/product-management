using ProductValidation.Models;
using ProductValidation.DTOs.User;

namespace ProductValidation.Services.Interfaces
{
    public interface IUserSetService
    {
        public Task<User> CreateUserService(CreateUserDto createUserDto);
    }
}