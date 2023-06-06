using Bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<Genre?> GetGenreById(int id)
        {
            return await _genreRepository.GetById(id);
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _genreRepository.GetAll();
        }

        public async Task<int> AddGenre(Genre genre)
        {
            return await _genreRepository.Create(genre);
        }

        public async Task<bool> UpdateGenre(int id, Genre genre)
        {
            return await _genreRepository.Update(id, genre);
        }

        public async Task<bool> DeleteGenre(int id)
        {
            return await _genreRepository.Delete(id);
        }
    }
}
