using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Context;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<TaskItem> GetTaskItemsOfUser(string UserName)
        {
            return (List<TaskItem>)_context.Users.FirstOrDefault(u => u.Name == UserName).TaskList;
        }
    }
}
