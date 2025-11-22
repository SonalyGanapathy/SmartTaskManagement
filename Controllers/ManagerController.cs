using System;
using System.Linq;
using System.Web.Mvc;
using SmartTaskManagement.Data;
using SmartTaskManagement.Models;
using SmartTaskManagement.Models.ViewModels;
using System.Data.Entity;


namespace SmartTaskManagement.Data
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult AssignTask()
        {
            System.Diagnostics.Debug.WriteLine("---- GET /Manager/AssignTask started ----");

            var managerEmail = Session["Email"]?.ToString();
            System.Diagnostics.Debug.WriteLine($"Session Email: {managerEmail}");

            var manager = _db.Users.FirstOrDefault(u => u.Email == managerEmail);
            if (manager == null)
            {
                System.Diagnostics.Debug.WriteLine("❌ Manager not found, redirecting to login.");
                return RedirectToAction("Login", "Account");
            }

            var dept = manager.Department;
            System.Diagnostics.Debug.WriteLine($"Manager Department: {dept}");

            var tasks = _db.TaskItems
    .Include(t => t.AssignedToUser)
    .Include(t => t.AssignedByUser)
                .Where(t => t.Department == dept)
                .OrderByDescending(t => t.UpdatedAt)
                .ToList();


            foreach (var t in tasks)
            {
                System.Diagnostics.Debug.WriteLine("LOADED EMPLOYEE: " + t.AssignedToUser?.Name);
            }


            System.Diagnostics.Debug.WriteLine($"✅ Loaded {tasks.Count} tasks for department: {dept}");

            var employees = _db.Users
                .Where(u => u.Department == dept && u.Role == "Employee")
                .ToList();

            var viewModel = new AssignTaskViewModel
            {
                EmployeesInDepartment = employees
                    .Select(e => new SelectListItem
                    {
                        Value = e.UserId.ToString(),
                        Text = e.Name
                    })
                    .ToList(),
                Status = "ToDo",
                Department = dept
            };

            viewModel.DepartmentTasks = tasks;
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignTask(AssignTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AssignTask");
            }

            var managerEmail = Session["Email"]?.ToString();
            var manager = _db.Users.FirstOrDefault(u => u.Email == managerEmail);

            if (manager == null)
            {
                System.Diagnostics.Debug.WriteLine("❌ Manager not found in POST.");
                return RedirectToAction("Login", "Account");
            }

            int managerId = manager.UserId;
            string dept = manager.Department; // ✅ same as in GET

            var task = new TaskItem
            {
                ProjectName = model.ProjectName,
                Description = model.Description,
                Status = model.Status,
                EstimatedTime = model.EstimatedTime,
                AssignedToUserId = model.AssignedTo,
                AssignedByUserId = managerId,
                Department = dept, // ✅ consistent department
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _db.TaskItems.Add(task);
            _db.SaveChanges();

            System.Diagnostics.Debug.WriteLine($"✅ Task saved successfully! Department: {dept}");
            TempData["Success"] = "Task assigned successfully!";

            return RedirectToAction("AssignTask");
        }

        public ActionResult ReviewTask(int id)
        {
            var task = _db.TaskItems
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser) 
                .FirstOrDefault(t => t.TaskId == id);

            if (task == null)
                return HttpNotFound();

            return View(task);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReviewTask(int taskId, string reviewStatus, string managerComments)
        {
            var task = _db.TaskItems.FirstOrDefault(t => t.TaskId == taskId);
            if (task == null)
                return HttpNotFound();

            task.ReviewStatus = reviewStatus;
            task.ManagerComments = managerComments;
            task.UpdatedAt = DateTime.Now;

            _db.SaveChanges();

            return RedirectToAction("AssignTask");
        }

    }
}

