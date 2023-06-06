using Bookstore.Models;

namespace Bookstore.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<int> AddAuthor(Author author);
        Task<bool> DeleteAuthor(int id);
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<IEnumerable<Author>> GetAllAuthorsSortedByDateOfBirth(bool isDescending);
        Task<IEnumerable<Author>> GetAllAuthorsSortedBySurname(bool isDescending);
        Task<Author?> GetAuthorById(int id);
        Task<bool> UpdateAuthor(int id, Author author);
    }
}