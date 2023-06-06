using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repositories.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext _context;

        public BookRepository(BookstoreDbContext context) => _context = context;

        public async Task<IEnumerable<Book>> GetAll() => await _context.Books.ToListAsync();

        public async Task<Book?> GetById(int id)
        {
            Book? book = await _context.Books.FindAsync(id);
            return book;
        }

        public async Task<int> Create(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return (await _context.Books.FindAsync(book.Id)).Id;
        }

        public async Task<bool> Update(int id, Book book)
        {
            if (id != book.Id) return false;

            var modifiedBook = _context.Books.Find(id);
            if (modifiedBook != null)
            {
                modifiedBook.Title = book.Title;
                modifiedBook.Price = book.Price;
                modifiedBook.PublishedDate = book.PublishedDate;
                modifiedBook.PageNumber = book.PageNumber;
                modifiedBook.Authors = book.Authors;
                modifiedBook.Genres = book.Genres;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            Book? book = await _context.Books.FindAsync(id);

            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
