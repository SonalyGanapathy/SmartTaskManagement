using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManagement.Models
{
    [Table("AppUsers")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }

        // Tasks assigned TO this user
        public virtual ICollection<TaskItem> TasksAssignedToMe { get; set; }
            = new List<TaskItem>();

        // Tasks assigned BY this user (e.g., manager)
        public virtual ICollection<TaskItem> TasksAssignedByMe { get; set; }
            = new List<TaskItem>();
    }

}
