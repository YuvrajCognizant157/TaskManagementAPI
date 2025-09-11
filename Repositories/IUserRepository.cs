using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface IUserRepository
    {
        public User GetUserByName(string name, string password);

        public int AddUser(RegisterDTO registerDTO);
    }
}
