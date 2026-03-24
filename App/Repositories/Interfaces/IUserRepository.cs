using ProductValidation.Models;

namespace ProductValidation.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Create(User user);
    }
}