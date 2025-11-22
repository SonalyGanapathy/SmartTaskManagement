using SmartTaskManagement.Data;
using SmartTaskManagement.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartTaskManagement.Data
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // ✅ View all tasks for logged-in employee
        //public ActionResult ViewTasks()
        //{
        //    var email = Session["Email"]?.ToString();
        //    var user = _db.Users.FirstOrDefault(u => u.Email == email);

        //    var tasks = _db.TaskItems
        //        .Include(t => t.AssignedByUser)
        //        .Include(t => t.AssignedToUser)
        //        .Where(t => t.AssignedToUserId == user.UserId)
        //        .ToList();
        //    System.Diagnostics.Debug.WriteLine("AssignedByUser.Name: " + tas);
        //    return View(tasks);
        //}

        public ActionResult ViewTasks()
        {
            System.Diagnostics.Debug.WriteLine("===== DEBUG: ViewTasks() =====");

            var email = Session["Email"]?.ToString();
            var user = _db.Users.FirstOrDefault(u => u.Email == email);

            var tasks = _db.TaskItems
                .Include(t => t.AssignedByUser)
                .Include(t => t.AssignedToUser)
                .Where(t => t.AssignedToUserId == user.UserId)
                .ToList();

            System.Diagnostics.Debug.WriteLine("Total tasks: " + tasks.Count);

            foreach (var t in tasks)
            {
                System.Diagnostics.Debug.WriteLine("---- Task ----");
                System.Diagnostics.Debug.WriteLine("AssignedByUser.Name: " + t.AssignedByUser?.Name);
                System.Diagnostics.Debug.WriteLine("AssignedToUser.Name: " + t.AssignedToUser?.Name);
            }

            return View(tasks);
        }



        // ✅ GET: Employee/EditTask/5
        public ActionResult EditTask(int id)
        {
            var task = _db.TaskItems.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
                return HttpNotFound();

            // Ensure only the assigned employee can edit
            int userId = Convert.ToInt32(Session["UserId"]);
            if (task.AssignedToUserId != userId)
                return new HttpUnauthorizedResult("You cannot edit someone else's task.");

            return View(task);
        }

        // ✅ POST: Employee/EditTask (update description and status)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask(TaskItem model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingTask = _db.TaskItems.Find(model.TaskId);
            if (existingTask == null)
                return HttpNotFound();

            // Ensure the employee owns this task
            int userId = Convert.ToInt32(Session["UserId"]);
            if (existingTask.AssignedToUserId != userId)
                return new HttpUnauthorizedResult("You cannot edit someone else's task.");

            // ✅ Update only allowed fields
            existingTask.Description = model.Description;
            existingTask.Status = model.Status;
            existingTask.UpdatedAt = DateTime.UtcNow;

            _db.Entry(existingTask).State = EntityState.Modified;
            _db.SaveChanges();

            TempData["Success"] = "Task updated successfully!";
            return RedirectToAction("ViewTasks");
        }
    }
}
