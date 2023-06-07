using Bookstore.Models;

namespace Bookstore.Services.Interfaces
{
    public interface IBookService
    {
        Task<int> AddBook(Book book);
        Task<bool> DeleteBook(int id);
        Task<IEnumerable<Book?>> GetAllBooks();
        Task<IEnumerable<Book?>> GetAllBooksSortedByPrice(bool isDescending);
        Task<IEnumerable<Book?>> GetAllBooksSortedByTitle(bool isDescending);
        Task<Book?> GetBookById(int id);
        Task<IEnumerable<Book?>> GetBooksByAuthor(Author author);
        Task<IEnumerable<Book?>> GetBooksByGenre(Genre genre);
        Task<IEnumerable<Book?>> GetBooksByPageCount(int count);
        Task<bool> UpdateBook(int id, Book book);
    }
}