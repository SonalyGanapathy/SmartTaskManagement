using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SmartTaskManagement.Models.ViewModels
{
    public class AssignTaskViewModel
    { 
        public int? TaskId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; } = "ToDo";

        [Required]
        public decimal EstimatedTime { get; set; }

        [Required]
        public int AssignedTo { get; set; }

        public string Department { get; set; }

        public IEnumerable<SelectListItem> EmployeesInDepartment { get; set; }

        public System.Collections.Generic.List<TaskItem> DepartmentTasks { get; set; }
    }
}
