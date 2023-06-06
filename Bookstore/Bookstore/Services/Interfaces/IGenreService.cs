using Bookstore.Models;

namespace Bookstore.Services.Interfaces
{
    public interface IGenreService
    {
        Task<int> AddGenre(Genre genre);
        Task<bool> DeleteGenre(int id);
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<Genre?> GetGenreById(int id);
        Task<bool> UpdateGenre(int id, Genre genre);
    }
}