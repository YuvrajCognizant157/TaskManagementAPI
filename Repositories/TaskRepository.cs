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
        public List<TaskItem> GetTaskItemsOfUser(int UserId)
        {
            return (List<TaskItem>)_context.TaskItems.Where(t => t.UserId == UserId).ToList();
        }
    }
}
