using System.Collections.Generic;
using SmartTaskManagement.Models;

public class ViewTasksViewModel
{
    public IEnumerable<TaskItem> Tasks { get; set; }
    public string CurrentUserDepartment { get; set; }
    public int CurrentUserId { get; set; }
}
