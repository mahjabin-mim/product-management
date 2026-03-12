using ProductValidation.Models;

namespace ProductValidation.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Create(User user);
    }
}