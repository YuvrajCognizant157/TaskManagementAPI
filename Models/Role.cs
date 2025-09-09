using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
