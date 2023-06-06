using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookstoreDbContext _context;

        public UserRepository(BookstoreDbContext context) => _context = context;

        public async Task<IEnumerable<User?>> GetAll() => await _context.Users.ToListAsync();

        public async Task<int> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return (await _context.Users.FindAsync(user.Id)).Id;
        }
    }
}
