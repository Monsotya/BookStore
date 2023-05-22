using Bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Register(User user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<User> Login(UserLogin login)
        {
            var users = await Task.Run(() => _userRepository.GetAll().Result);
            var user = users.Where(x => x.UserName.ToLower() == login.UserName.ToLower() && x.Password == login.Password).FirstOrDefault();

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
