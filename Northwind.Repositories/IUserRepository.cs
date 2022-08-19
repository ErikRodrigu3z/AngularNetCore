using Northwin.Models;

namespace Northwind.Repositories
{
    public interface IUserRepository : IRepository<User> 
    {
        User ValidateUser(string email, string password);
    }
}
