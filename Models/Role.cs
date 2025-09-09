using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
