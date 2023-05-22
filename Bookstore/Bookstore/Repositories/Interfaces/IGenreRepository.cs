using Bookstore.Models;

namespace Bookstore.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<int> Create(Genre genre);
        Task<bool> Delete(int id);
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(int id);
        Task<bool> Update(int id, Genre genre);
    }
}