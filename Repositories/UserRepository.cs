using ProductValidation.Data;
using ProductValidation.Models;
using ProductValidation.Repositories.Interfaces;

namespace ProductValidation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public User Create(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }
    }
}