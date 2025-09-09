using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementAPI.Models
{
    public class TaskItem
    {
        //Title, Description, Status, AssignedTo)
        [Key]
        [Required(ErrorMessage ="Key Value cannot be NULL.")]
        public int TaskItemId { get; set; }

        [StringLength(20,ErrorMessage ="Length of Title out of range.")]
        public string Title { get; set; }
        [StringLength(100, ErrorMessage = "Length of Title out of range.")]
        public string Description { get; set; }
        public int Status { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [NotMapped]
        public virtual User User { get; set; }
    }
}
