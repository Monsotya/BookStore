using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Repositories.Interfaces;

namespace Bookstore.Repositories.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookstoreDbContext _context;

        public AuthorRepository(BookstoreDbContext context) => _context = context;

        public async Task<IEnumerable<Author>> GetAll() => await _context.Authors.ToListAsync();

        public async Task<Author> GetById(int id)
        {
            Author author = await _context.Authors.FindAsync(id);
            return author == null ? null : author;
        }

        public async Task<int> Create(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return _context.Authors.FindAsync(author.Id).Result.Id;
        }

        public async Task<bool> Update(int id, Author author)
        {
            if (id != author.Id) return false;

            var modifiedAuthor = _context.Authors.Find(id);
            if (modifiedAuthor != null)
            {
                modifiedAuthor.Name = author.Name;
                modifiedAuthor.Surname = author.Surname;
                modifiedAuthor.DateOfBirth = author.DateOfBirth;
                modifiedAuthor.DateOfDeath = author.DateOfDeath;
                modifiedAuthor.Books = author.Books;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            Author author = await _context.Authors.FindAsync(id);

            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
