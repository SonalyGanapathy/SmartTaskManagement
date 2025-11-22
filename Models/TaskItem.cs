using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManagement.Models
{
    public class TaskItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public decimal EstimatedTime { get; set; }

        // ✅ Foreign key fields (must match navigation property attributes)
        [Required]
        public int AssignedToUserId { get; set; }

        [Required]
        public int AssignedByUserId { get; set; }

        public string Department { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // ✅ Manager review
        public string ReviewStatus { get; set; } = "Pending";
        public string ManagerComments { get; set; }

        // ✅ Correct foreign key relationships
        [ForeignKey(nameof(AssignedToUserId))]
        public virtual User AssignedToUser { get; set; }

        [ForeignKey(nameof(AssignedByUserId))]
        public virtual User AssignedByUser { get; set; }
    }
}
