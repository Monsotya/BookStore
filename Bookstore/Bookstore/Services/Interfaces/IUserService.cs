using Bookstore.Models;

namespace Bookstore.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Login(UserLogin login);
        Task<int> Register(User user);
    }
}