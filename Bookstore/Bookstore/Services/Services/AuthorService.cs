using Bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            return await _authorRepository.GetById(id);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsSortedBySurname(bool isDescending)
        {
            if (isDescending)
            {
                return (await _authorRepository.GetAll()).OrderByDescending(x => x.Surname);
            }
            return (await _authorRepository.GetAll()).OrderBy(x => x.Surname);
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsSortedByDateOfBirth(bool isDescending)
        {
            if (isDescending)
            {
                return (await _authorRepository.GetAll()).OrderByDescending(x => x.DateOfBirth);
            }
            return (await _authorRepository.GetAll()).OrderBy(x => x.DateOfBirth);
        }

        public async Task<int> AddAuthor(Author author)
        {
            return await _authorRepository.Create(author);
        }

        public async Task<bool> UpdateAuthor(int id, Author author)
        {
            return await _authorRepository.Update(id, author);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            return await _authorRepository.Delete(id);
        }

        public async Task<IEnumerable<object>> GetAuthorsWithBooksCount()
        {
            return await _authorRepository.GetAuthorsWithBooksCount();
        }
    }
}
