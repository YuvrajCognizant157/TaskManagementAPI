using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DOB { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<TaskItem> TaskList { get; set; }
    }
}
