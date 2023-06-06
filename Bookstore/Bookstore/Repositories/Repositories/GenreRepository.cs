using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Repositories.Interfaces;

namespace Bookstore.Repositories.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookstoreDbContext _context;

        public GenreRepository(BookstoreDbContext context) => _context = context;

        public async Task<IEnumerable<Genre>> GetAll() => await _context.Genres.ToListAsync();

        public async Task<Genre?> GetById(int id)
        {
            Genre? genre = await _context.Genres.FindAsync(id);
            return genre;
        }

        public async Task<int> Create(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return (await _context.Genres.FindAsync(genre.Id)).Id;
        }

        public async Task<bool> Update(int id, Genre genre)
        {
            if (id != genre.Id) return false;

            var modifiedGenre = _context.Genres.Find(id);
            if (modifiedGenre != null)
            {
                modifiedGenre.Name = genre.Name;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            Genre? genre = await _context.Genres.FindAsync(id);

            if (genre == null) return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
