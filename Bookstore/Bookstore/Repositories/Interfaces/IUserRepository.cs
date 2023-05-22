using Bookstore.Models;

namespace Bookstore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<int> Create(User user);
        Task<IEnumerable<User>> GetAll();
    }
}