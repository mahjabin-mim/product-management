using ProductValidation.Models;

namespace ProductValidation.Services.Interfaces
{
    public interface IUserSetService
    {
        User CreateUserService(CreateUserDto createUserDto);
    }
}