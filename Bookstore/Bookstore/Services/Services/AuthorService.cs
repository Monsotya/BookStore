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

        public async Task<Author> GetAuthorById(int id)
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
                return await Task.Run(() => _authorRepository.GetAll().Result.OrderByDescending(x => x.Surname));
            }
            return await Task.Run(() => _authorRepository.GetAll().Result.OrderBy(x => x.Surname));
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsSortedByDateOfBirth(bool isDescending)
        {
            if (isDescending)
            {
                return await Task.Run(() => _authorRepository.GetAll().Result.OrderByDescending(x => x.DateOfBirth));
            }
            return await Task.Run(() => _authorRepository.GetAll().Result.OrderBy(x => x.DateOfBirth));
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
    }
}
