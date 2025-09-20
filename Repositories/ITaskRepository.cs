using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface ITaskRepository
    {
        public List<TaskItem> GetTaskItemsOfUser(int UserId);
    }
}
