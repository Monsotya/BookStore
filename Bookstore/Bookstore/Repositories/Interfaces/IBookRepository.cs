using Bookstore.Models;

namespace Bookstore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<int> Create(Book book);
        Task<bool> Delete(int id);
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task<bool> Update(int id, Book book);
    }
}