using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementAPI.DTOs
{
    public class RegisterDTO
    {
        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DOB { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
    }
}
