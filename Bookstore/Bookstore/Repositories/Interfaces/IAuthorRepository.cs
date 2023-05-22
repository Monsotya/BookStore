using Bookstore.Models;

namespace Bookstore.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<int> Create(Author author);
        Task<bool> Delete(int id);
        Task<IEnumerable<Author>> GetAll();
        Task<Author> GetById(int id);
        Task<bool> Update(int id, Author author);
    }
}