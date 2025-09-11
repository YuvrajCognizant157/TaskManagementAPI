using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Context;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class userRepository : IUserRepository
    {
        private readonly AppDbContext _appContext;

        public userRepository(AppDbContext appDbContext)
        {
            _appContext = appDbContext;
        }

        public User GetUserByName(string name, string password)
        {
            return _appContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Name == name && u.password == password);
        }

        public int AddUser(RegisterDTO registerDTO)
        {
            User user = new User
            {
                Name = registerDTO.Name,
                Address = registerDTO.Address,
                DOB = registerDTO.DOB,
                password = registerDTO.password,
                RoleId = registerDTO.RoleId

            };
            _appContext.Users.Add(user);
            return _appContext.SaveChanges();

        }


    }
}
