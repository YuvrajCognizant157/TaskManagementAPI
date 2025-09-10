using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTOs
{
    public class LoginDTO
    {
        public string name { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
