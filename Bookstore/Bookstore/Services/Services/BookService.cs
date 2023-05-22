using Bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<IEnumerable<Book>> GetAllBooksSortedByTitle(bool isDescending)
        {
            if (isDescending)
            {
                return await Task.Run(() => _bookRepository.GetAll().Result.OrderByDescending(x => x.Title));
            }
            return await Task.Run(() => _bookRepository.GetAll().Result.OrderBy(x => x.Title));
        }

        public async Task<IEnumerable<Book>> GetAllBooksSortedByPrice(bool isDescending)
        {
            if (isDescending)
            {
                return await Task.Run(() => _bookRepository.GetAll().Result.OrderByDescending(x => x.Price));
            }
            return await Task.Run(() => _bookRepository.GetAll().Result.OrderBy(x => x.Price));
        }

        public async Task<IEnumerable<Book>> GetBooksByGenre(Genre genre)
        {
            return await Task.Run(() => _bookRepository.GetAll().Result.Where(x => x.Genres.Contains(genre)).ToList());
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthor(Author author)
        {
            return await Task.Run(() => _bookRepository.GetAll().Result.Where(x => x.Authors.Contains(author)).ToList());
        }

        public async Task<int> AddBook(Book book)
        {
            return await _bookRepository.Create(book);
        }

        public async Task<bool> UpdateBook(int id, Book book)
        {
            return await _bookRepository.Update(id, book);
        }

        public async Task<bool> DeleteBook(int id)
        {
            return await _bookRepository.Delete(id);
        }
    }
}
