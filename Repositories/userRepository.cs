using TaskManagementAPI.Context;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class userRepository
    {
        private readonly AppDbContext _appContext;

        public userRepository(AppDbContext appDbContext)
        {
            _appContext = appDbContext;
        }

        public User GetUserByName(string name,string password)
        {
            return _appContext.Users.FirstOrDefault(u => u.Name == name && u.password ==password);
        }
    }
}
