using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ITaskRepository taskRepo;
        public EmployeeController(ITaskRepository repository)
        {
            taskRepo = repository;
        }
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("/get-taskitem")]
        public IActionResult GetTaskItem([FromQuery] int UserId)
        {
            var items = taskRepo.GetTaskItemsOfUser(UserId);
            if (items == null) return Ok("No items");
            else return Ok(new Dictionary<string, object>
            {
                { "TaskItems", items },
                { "Count", items.Count }
            });
        }
    }
}
